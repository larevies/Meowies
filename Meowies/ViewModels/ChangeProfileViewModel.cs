using System;
using System.Linq;
using System.Windows.Input;
using Meowies.Models;
using ReactiveUI;

namespace Meowies.ViewModels;

public class ChangeProfileViewModel : ProfileViewModelBase
{
    public ChangeProfileViewModel()
    {
        ChangeNameCommand = ReactiveCommand.Create(ChangeName);
        ChangeEmailCommand = ReactiveCommand.Create(ChangeEmail);
        ChangePasswordCommand = ReactiveCommand.Create(ChangePassword);
        ChangedNameCommand = ReactiveCommand.Create(ChangedName);
        ChangedEmailCommand = ReactiveCommand.Create(ChangedEmail);
        ChangedPasswordCommand = ReactiveCommand.Create(ChangedPassword);
        GoBackCommand = ReactiveCommand.Create(GoBack);
        EnterCommand = ReactiveCommand.Create(Enter);
    }

    public string Welcome { get; set; } = "Here are three things I recommend you to do:\n" +
                                          "1. Let fate decide what to watch today\n" +
                                          "2. Read facts about your favorite actor\n" +
                                          "3. Add something new to bookmarks\n" +
                                          "Don't feel like it? Maybe you want to change\n" +
                                          "something in your profile?";

    public ICommand ChangeNameCommand { get; }
    private void ChangeName()
    {
        ChangingName = true;
    }
    
    public ICommand ChangeEmailCommand { get; }
    private void ChangeEmail()
    {
        ChangingEmail = true;
    }
    
    public ICommand ChangePasswordCommand { get; }
    private void ChangePassword()
    {
        ChangingPassword = true;
    }
    public string NewName { get; set; } = null!;
    public string NewEmail { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    
    public ICommand ChangedNameCommand { get; }
    private void ChangedName()
    {
        ChangingName = false; 
        var context = new MeowiesContext();
        User? queryable = context.Users
            .FirstOrDefault(x => x.Email == SignInViewModel
                .MailAddress && x.Password == SignInViewModel.Password);
        queryable!.Name = NewName;
        context.SaveChanges();
        CurrentUser.Name = queryable.Name;
    }
    
    public ICommand ChangedEmailCommand { get; }

    private void ChangedEmail()
    {
        try
        {
            ChangingEmail = false;
            var context = new MeowiesContext();
            User? queryable = context.Users
                .FirstOrDefault(x => x.Email == SignInViewModel
                    .MailAddress && x.Password == SignInViewModel.Password);
            if (queryable!.Email == NewEmail)
            {
                throw new Exception("This email is literally the fucking same u bullshit!!!!");
            }
            if (_alreadyExists(NewEmail))
            {
                throw new Exception("This email already exists!!!");
            }
            CurrentUser.Email = queryable.Email;
            queryable.Email = NewEmail;
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private bool _alreadyExists(string email)
    {
        var context = new MeowiesContext();
        if (context.Users.FirstOrDefault(x => x.Email == email) is null) return false;
        return true;
    }

    public ICommand ChangedPasswordCommand { get; }
    private void ChangedPassword()
    {
        ChangingPassword = false;
        var context = new MeowiesContext();
        User? queryable = context.Users
            .FirstOrDefault(x => x.Email == SignInViewModel
                .MailAddress && x.Password == SignInViewModel.Password);
        queryable!.Password = NewPassword;
        context.SaveChanges();
        CurrentUser.Password = queryable.Password;
        Console.WriteLine(CurrentUser.Password);
    }
    private bool _changingName;
    public bool ChangingName { 
        get => _changingName;
        set
        {
            _changingName = value;
            OnPropertyChanged(nameof(ChangingName));
        }
    } 
    private bool _changingEmail;
    public bool ChangingEmail { 
        get => _changingEmail;
        set
        {
            _changingEmail = value;
            OnPropertyChanged(nameof(ChangingEmail));
        }
    } 
    private bool _changingPassword;
    public bool ChangingPassword { 
        get => _changingPassword;
        set
        {
            _changingPassword = value;
            OnPropertyChanged(nameof(ChangingPassword));
        }
    }
    private bool _entered;
    public bool Entered { 
        get => _entered;
        set
        {
            _entered = value;
            OnPropertyChanged(nameof(Entered));
        }
    }

    public ICommand GoBackCommand { get; }
    public void GoBack()
    {
        Entered = false;
    }

    public ICommand EnterCommand { get; }
    private void Enter()
    {
        Entered = true;
    }

    private User _currentUser = null!;

    public User CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            OnPropertyChanged(nameof(CurrentUser));
        }
    }

    public override bool CanNavigateNext
    {
        get => false;
        protected set => throw new NotSupportedException();
    }
    public override bool CanNavigatePrevious => false;
}