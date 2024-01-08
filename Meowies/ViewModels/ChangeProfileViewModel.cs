using System;

namespace Meowies.ViewModels;

public class ChangeProfileViewModel : ProfileViewModelBase
{
    public static string UserName => ProfileViewModel.UserName;
    public override bool CanNavigateNext
    {
        get => false;
        protected set => throw new NotSupportedException();
    }
    public override bool CanNavigatePrevious => false;
}