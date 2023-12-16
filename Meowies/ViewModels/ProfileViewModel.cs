using System;
using System.Linq;
using System.Windows.Input;
using DynamicData;
using Meowies;
using Meowies.Models;
using ReactiveUI;

namespace Meowies.ViewModels;

public partial class ProfileViewModel : PageViewModelBase
{
    public ProfileViewModel()
    {
        
        CurrentProfile = _profilePages[0];
        
        var canNavNext = this.WhenAnyValue(x => x.CurrentProfile.CanNavigateNext);
        var canNavPrev = this.WhenAnyValue(x => x.CurrentProfile.CanNavigatePrevious);
        
        NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
        NavigatePreviousCommand = ReactiveCommand.Create(NavigatePrevious, canNavPrev);
    }
    
    private string _next = "Sign up";
    private string _previous = "Sign in";
    public string Next
    {
        get => _next;
        set => this.RaiseAndSetIfChanged(ref _next, value);
    }
    public string Previous
    {
        get => _previous;
        set => this.RaiseAndSetIfChanged(ref _previous, value);
    }
    private readonly ProfileViewModelBase[] _profilePages = 
    { 
        new WelcomeViewModel(),
        new SignUpViewModel(),
        new SignInViewModel(),
        new ChangeProfileViewModel()
    };
    private ProfileViewModelBase _currentProfile;
    public ProfileViewModelBase CurrentProfile
    {
        get => _currentProfile;
        private set => this.RaiseAndSetIfChanged(ref _currentProfile, value);
    }
    public ICommand NavigateNextCommand { get; }

    private void NavigateNext()
    {
        var index = _profilePages.IndexOf(CurrentProfile) + 1;
        CurrentProfile = _profilePages[index];
        if (CurrentProfile == _profilePages[2])
        {
            Next = "Sign in";
            Previous = "Go back";
            using var context = new MeowiesContext();
            context.Users.Add(SignUpViewModel.NewUser);
            context.SaveChanges();
        }
        else if (CurrentProfile == _profilePages[0])
        {
            Next = "Sign up";
            Previous = "Sign in";
        }
        else if (CurrentProfile == _profilePages[1])
        {
            Next = "Sign up";
            Previous = "Go Back";
        } 
        else if (CurrentProfile == _profilePages[3])
        {
            using var context = new MeowiesContext();
            var queryable = context.Users
                .FirstOrDefault(x => x.Email == SignInViewModel.MailAddress && x.Password == SignInViewModel.Password);
            //Console.Write(queryable.Name + "\n");
            try
            {
                if (queryable.Name == null || queryable.Name == "") return;
                Console.Write("success");
                CurrentProfile = _profilePages[3];
            }
            catch (Exception e)
            {
                Console.Write("fail");
                CurrentProfile = _profilePages[2];
            }
        }
    }
    public ICommand NavigatePreviousCommand { get; }

    private void NavigatePrevious()
    {
        int index;
        if (CurrentProfile == _profilePages[0])
        {
            index = _profilePages.IndexOf(CurrentProfile) + 2;
        }
        else
        {
            index = 0;
        }

        CurrentProfile = _profilePages[index];
        if (CurrentProfile == _profilePages[2])
        {
            Next = "Sign in";
            Previous = "Go back";
        }
        else if (CurrentProfile == _profilePages[0])
        {
            Next = "Sign up";
            Previous = "Sign in";
        }
        else if (CurrentProfile == _profilePages[1])
        {
            Next = "Sign up";
            Previous = "Go Back";
        }
    }
    public override bool CanCat => true;
    public override bool CanSearch => true;
    public override bool CanRandom => true;
    public override bool CanFavourites => true;
    public override bool CanTrending => true;
}