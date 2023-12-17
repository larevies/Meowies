using System.ComponentModel.DataAnnotations;
using Meowies.Models;
using ReactiveUI;

namespace Meowies.ViewModels;

public class SignInViewModel : ProfileViewModelBase
{
    /*public SignInViewModel()
    {
        using var context = new MeowiesContext();
        this.WhenAnyValue(x => x.MailAddress, x => x.Password)
            .Subscribe(_ => UpdateCanNavigateNext());
    }*/

    public static string Message { get; set; } = "";
    [Required] [EmailAddress] public static string MailAddress { get; set; } = null!;
    [Required] public static string Password { get; set; } = null!;

    private void UpdateCanNavigateNext()
    {
        CanNavigateNext = 
            !string.IsNullOrEmpty(MailAddress) 
            && MailAddress.Contains("@")
            && !string.IsNullOrEmpty(Password);
    }
    private bool _canNavigateNext;
    public override bool CanNavigateNext
    {
        get => true;
        protected set => this.RaiseAndSetIfChanged(ref _canNavigateNext, value);
    }
    public override bool CanNavigatePrevious => true;
    public static User CurrentUser { get; set; } = null!;
}
