using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Meowies.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace Meowies.ViewModels;

public class SignInViewModel : ProfileViewModelBase
{
    public SignInViewModel()
    {
        using var context = new MeowiesContext();
        var matchingUser = context.Users.FirstOrDefault(x => x.Email == MailAddress);
        this.WhenAnyValue(x => x.MailAddress, x => x.Password)
            .Subscribe(_ => UpdateCanNavigateNext());
        
    }

    private string? _mailAddress;
    [Required]
    [EmailAddress]
    public string? MailAddress
    {
        get => _mailAddress;
        set => this.RaiseAndSetIfChanged(ref _mailAddress, value);
    }

    private string? _password;
    [Required]
    public string? Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
    
    private void UpdateCanNavigateNext()
    {
        CanNavigateNext = 
            !string.IsNullOrEmpty(_mailAddress) 
            && _mailAddress.Contains("@")
            && !string.IsNullOrEmpty(_password);
    }
    private bool _canNavigateNext;
    public override bool CanNavigateNext
    {
        get => _canNavigateNext;
        protected set => this.RaiseAndSetIfChanged(ref _canNavigateNext, value);
    }
    public override bool CanNavigatePrevious => true;
}
