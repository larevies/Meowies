using System;
using System.ComponentModel.DataAnnotations;
using ReactiveUI;

namespace Meowies.ViewModels;

public class SignInViewModel : PageViewModelBase
{
    public SignInViewModel()
    {
        this.WhenAnyValue(x => x.MailAddress, x => x.Password)
            .Subscribe(_ => UpdateCanNavigateNext());
    }


    private string? _mailAddress;
    [Required]
    [EmailAddress]
    public string? MailAddress
    {
        get { return _mailAddress; }
        set { this.RaiseAndSetIfChanged(ref _mailAddress, value); }
    }


    private string? _password;
    [Required]
    public string? Password
    {
        get { return _password; }
        set { this.RaiseAndSetIfChanged(ref _password, value); }
    }


    private bool _canNavigateNext;
    public override bool CanNavigateNext
    {
        get => _canNavigateNext;
        protected set => this.RaiseAndSetIfChanged(ref _canNavigateNext, value);
    }
    public override bool CanNavigatePrevious
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    private void UpdateCanNavigateNext()
    {
        CanNavigateNext = 
            !string.IsNullOrEmpty(_mailAddress) 
            && _mailAddress.Contains("@")
            && !string.IsNullOrEmpty(_password);
    }
}
