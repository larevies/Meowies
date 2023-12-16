using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Meowies.Models;
using ReactiveUI;

namespace Meowies.ViewModels;

public class SignUpViewModel : ProfileViewModelBase
{
    /*public SignUpViewModel()
    {
        this.WhenAnyValue(x => x.MailAddress, x => x.Password)
            .Subscribe(_ => UpdateCanNavigateNext());
    }*/

    /*public SignUpViewModel()
    {
        var user = new User() { Name = Name, Email = MailAddress, Birthday = Birthday, Password = Password};
    }*/

    [Required]
    [EmailAddress]
    public static string MailAddress { get; set; } = "";

    [Required]
    public static string Password { get; set; } = "";

    [Required]
    public static string Name { get; set; } = "";

    //[Required]
    public static DateTime Birthday { get; set; } = DateTime.Today;

    private void UpdateCanNavigateNext()
    {
        if (!string.IsNullOrEmpty(MailAddress) 
            && MailAddress.Contains("@")
            && !string.IsNullOrEmpty(Password)
            && !string.IsNullOrEmpty(Name))
        {
            CanNavigateNext = true;
        }
    }
    
    private bool _canNavigateNext;
    public override bool CanNavigateNext
    {
        get => true;
        protected set => this.RaiseAndSetIfChanged(ref _canNavigateNext, value);
    }
    public override bool CanNavigatePrevious => true;

    public static User NewUser =>
        new()
        {
            Name = Name,
            Birthday = Birthday.ToString(CultureInfo.CurrentCulture),
            Password = Password,
            Email = MailAddress
        };
}