using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DynamicData;
using Meowies.Models;
using ReactiveUI;

namespace Meowies.ViewModels;

public class ProfileViewModel : ViewModelBase
{
    public ProfileViewModel()
    {
        CurrentProfile = Welcome;
        
        var canNavNext = this.WhenAnyValue(x => x.CurrentProfile.CanNavigateNext);
        var canNavPrev = this.WhenAnyValue(x => x.CurrentProfile.CanNavigatePrevious);
        
        NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
        NavigatePreviousCommand = ReactiveCommand.Create(NavigatePrevious, canNavPrev);
    }
    
    private string _next = "Sign up";
    public string Next
    {
        get => _next;
        set
        {
            _next = value;
            OnPropertyChanged(nameof(Next));
        } 
    }
    
    private string _previous = "Sign in";
    public string Previous
    {
        get => _previous;
        set
        {
            _previous = value;
            OnPropertyChanged(nameof(Previous));
        } 
    }

    private bool _areButtonsVisible = true;

    public bool AreButtonsVisible
    {
        get => _areButtonsVisible;
        set
        {
            _areButtonsVisible = value;
            OnPropertyChanged(nameof(AreButtonsVisible));
        }
    }

    #region Profile ViewModels
    
    private static readonly ProfileViewModelBase Welcome = new WelcomeViewModel();
    private static readonly ProfileViewModelBase SignUp = new SignUpViewModel();
    private static readonly ProfileViewModelBase SignIn = new SignInViewModel();
    private static readonly ChangeProfileViewModel ChangeProfile = new();
    
    private readonly ProfileViewModelBase[] _profilePages = 
    { 
        Welcome,
        SignUp,
        SignIn,
        ChangeProfile
    };
    
    #endregion
    
    private ProfileViewModelBase _currentProfile = null!;
    public ProfileViewModelBase CurrentProfile
    {
        get => _currentProfile;
        set
        {
            _currentProfile = value;
            OnPropertyChanged(nameof(CurrentProfile));
        }
    }
    public ICommand NavigateNextCommand { get; }

    private async void NavigateNext()
    {
        var index = _profilePages.IndexOf(CurrentProfile) + 1;
        CurrentProfile = _profilePages[index];
        
        if (CurrentProfile == SignIn)
        {
            Next = "Sign in";
            Previous = "Go back";
            using var context = new MeowiesContext();
            var queryable = context.Users.FirstOrDefault(x => x.Email == SignUpViewModel.MailAddress);
            
            if (queryable != null)
            {
                SignUpViewModel.Message = "This email is taken";
                CurrentProfile = SignUp;
            }
            else
            {
                context.Users.Add(SignUpViewModel.NewUser);
                await context.SaveChangesAsync();
            }
        }
        else if (CurrentProfile == Welcome)
        {
            Next = "Sign up";
            Previous = "Sign in";
        }
        else if (CurrentProfile == SignUp)
        {
            Next = "Sign up";
            Previous = "Go Back";
        } 
        else if (CurrentProfile == ChangeProfile)
        {
            using var context = new MeowiesContext();
            var queryable = context.Users
                .FirstOrDefault(x => x.Email == SignInViewModel
                    .MailAddress && x.Password == SignInViewModel.Password);
            try
            {
                AreButtonsVisible = false;
                SignInViewModel.CurrentUser = queryable ?? throw new InvalidOperationException();
                var queryableTwo = context.Bookmarks
                    .Where(o => o.User == SignInViewModel.CurrentUser)
                    .Select(o => o.MovieId)
                    .ToList();
                
                List<MovieItemDoc> userBookmarks = new(queryableTwo.Count);
                
                foreach (var movieId in queryableTwo)
                {
                    var task = JsonDeserializers.GetBmAsync(
                        Getters.GetMovieUrlById(
                            movieId.ToString()));
                    var item = await task!;
                    var doc = item!.docs[0]; 
                    userBookmarks.Add(doc);
                }
                
                // ChangeProfile.UserName = queryable.Name;
                ChangeProfile.CurrentUser = queryable;
                BookmarksViewModel.Bookmarks = userBookmarks;

                CurrentProfile = ChangeProfile;
            }
            catch (Exception e)
            {
                AreButtonsVisible = true;
                Console.WriteLine(e.Message);
                SignInViewModel.Message = "E-mail address or password do not match.\nTry again";
                CurrentProfile = SignIn;
            }
        }
    }
    
    public ICommand NavigatePreviousCommand { get; }

    private void NavigatePrevious()
    {
        int index;
        if (CurrentProfile == Welcome)
        {
            index = _profilePages.IndexOf(CurrentProfile) + 2;
        }
        else
        {
            index = 0;
        }

        CurrentProfile = _profilePages[index];
        if (CurrentProfile == SignIn)
        {
            Next = "Sign in";
            Previous = "Go back";
        }
        else if (CurrentProfile == Welcome)
        {
            Next = "Sign up";
            Previous = "Sign in";
        }
        else if (CurrentProfile == SignUp)
        {
            Next = "Sign up";
            Previous = "Go Back";
        }
    }
}
