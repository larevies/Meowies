using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Meowies.Models;
using ReactiveUI;

namespace Meowies.ViewModels;

public class ChangeProfileViewModel : ProfileViewModelBase
{
    public ChangeProfileViewModel()
    {
        ChangeNameCommand = ReactiveCommand.Create(ChangeName);
        ChangeEmailCommand = ReactiveCommand.Create(ChangeEmail);
        ChangedNameCommand = ReactiveCommand.Create(ChangedName);
        ChangedEmailCommand = ReactiveCommand.Create(ChangedEmail);
        ChangedPasswordCommand = ReactiveCommand.Create(ChangedPassword);
        GoBackToWelcomeCommand = ReactiveCommand.Create(GoBackToWelcome);
        GoBackToProfileCommand = ReactiveCommand.Create(GoBackToProfile);
        EnterCommand = ReactiveCommand.Create(Enter);
        StartPicChangingCommand = ReactiveCommand.Create(StartPicChanging);
        StartPasswordChangingCommand = ReactiveCommand.Create(StartPasswordChanging);
    }

    public string Welcome { get; set; } = "Here are three things I recommend you to do:\n" +
                                          "1. Let fate decide what to watch today\n" +
                                          "2. Read facts about your favorite actor\n" +
                                          "3. Add something new to bookmarks\n" +
                                          "Don't feel like it? Maybe you want to change\n" +
                                          "something in your profile?";

    private string _message = "";
    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }
    
    private string _emailMessage = "";
    public string EmailMessage
    {
        get => _emailMessage;
        set
        {
            _emailMessage = value;
            OnPropertyChanged(nameof(EmailMessage));
        }
    }
    
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
    public string NewName { get; set; } = null!;
    public string NewEmail { get; set; } = null!;
    private string _oldPassword = null!;

    public string OldPassword
    {
        get => _oldPassword;
        set
        {
            _oldPassword = value;
            OnPropertyChanged(nameof(OldPassword));
        }
    }
    private string _newPassword = null!;
    public string NewPassword
    {
        get => _newPassword;
        set
        {
            _newPassword = value;
            OnPropertyChanged(nameof(NewPassword));
        }
    }
    private string _newConfirmedPassword = null!;
    public string NewConfirmedPassword
    {
        get => _newConfirmedPassword;
        set
        {
            _newConfirmedPassword = value;
            OnPropertyChanged(nameof(NewConfirmedPassword));
        }
    }
    
    public ICommand ChangedNameCommand { get; }
    private async void ChangedName()
    {
        ChangingName = false;
        await MeowiesApiRequests.ChangeName(CurrentUser.Email, NewName);
        CurrentUser.Name = NewName;
        EmailMessage = "Success!";
        NewName = null!;
    }
    
    public ICommand ChangedEmailCommand { get; }

    private async void ChangedEmail()
    {
        try
        {
            
            ChangingEmail = false;
            await MeowiesApiRequests.GetUserFromDb(NewEmail);
            EmailMessage = "This email is taken!";
            
        }
        catch (Exception e)
        {
            Regex regex = new Regex(@"^[-\w.]+@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,4}$");
            if (regex.IsMatch(NewEmail)) {
                await MeowiesApiRequests.ChangeEmail(CurrentUser.Email, NewEmail);
                CurrentUser.Email = NewEmail;
                EmailMessage = "Success!";
                Console.WriteLine(e.Message);
            }
            else
            {
                EmailMessage = "Email isn't valid.";
            }
        }
        NewEmail = null!;
    }

    public ICommand ChangedPasswordCommand { get; }
    private async void ChangedPassword()
    {
        ChangingPassword = false;
        
        if (CurrentUser.Password == OldPassword && NewPassword == NewConfirmedPassword)
        {
            Message = "Success!";
            await MeowiesApiRequests.ChangePassword(CurrentUser.Email, NewPassword);
            CurrentUser.Password = NewPassword;
        }
        else
        {
            Message = "Passwords do not match";
        }
        
        OldPassword = null!;
        NewPassword = null!;
        NewConfirmedPassword = null!;

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
    private bool _entered = true;
    public bool Entered { 
        get => _entered;
        set
        {
            _entered = value;
            OnPropertyChanged(nameof(Entered));
        }
    }
    private bool _profileChanging;
    public bool ProfileChanging { 
        get => _profileChanging;
        set
        {
            _profileChanging = value;
            OnPropertyChanged(nameof(ProfileChanging));
        }
    }
    private bool _picChanging;
    public bool PicChanging { 
        get => _picChanging;
        set
        {
            _picChanging = value;
            OnPropertyChanged(nameof(PicChanging));
        }
    }
    private bool _passwordChanging;
    public bool PasswordChanging { 
        get => _passwordChanging;
        set
        {
            _passwordChanging = value;
            OnPropertyChanged(nameof(PasswordChanging));
        }
    }

    private Bitmap? _pic = ImageHelper.LoadFromResource(new Uri("avares://Meowies/Assets/Userpics/userpic2.png"));
    public Bitmap? Pic
    {
        get => _pic;
        set
        {
            _pic = value;
            OnPropertyChanged(nameof(Pic));
        }

    }
    public ICommand GoBackToWelcomeCommand { get; }

    private void GoBackToWelcome()
    {
        Entered = true;
        ProfileChanging = false;
    }
    public ICommand GoBackToProfileCommand { get; }

    private void GoBackToProfile()
    {
        ProfileChanging = true;
        PicChanging = false;
        PasswordChanging = false;
    }
    public ICommand EnterCommand { get; }
    private void Enter()
    {
        Entered = false;
        ProfileChanging = true;
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

    public ICommand StartPicChangingCommand { get; }
    private void StartPicChanging()
    {
        ProfileChanging = false;
        PicChanging = true;
    }

    public ICommand StartPasswordChangingCommand { get; }
    private void StartPasswordChanging()
    {
        ProfileChanging = false;
        PasswordChanging = true;
    }

    public async void SwitchPicture(int a)
    {
        Pic = ImageHelper.LoadFromResource(new Uri($"avares://Meowies/Assets/Userpics/userpic{a}.png"));
        await MeowiesApiRequests.ChangeProfPic(SignInViewModel.CurrentUser.Email, a);
        GoBackToProfile();
    }

    
    public void StartSwitchPicture(int a)
    {
        Pic = ImageHelper.LoadFromResource(new Uri($"avares://Meowies/Assets/Userpics/userpic{a}.png"));
    }
}