using System;
using Meowies.Models;

namespace Meowies.ViewModels;

public class ChangeProfileViewModel : ProfileViewModelBase
{
    private string _userName = "usEr";
    public string UserName 
    { 
        get => _userName;
        set => OnPropertyChanged(nameof(UserName));
    }
    public override bool CanNavigateNext
    {
        get => false;
        protected set => throw new NotSupportedException();
    }
    public override bool CanNavigatePrevious => false;
}