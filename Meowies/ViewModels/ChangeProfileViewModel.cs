using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;

namespace Meowies.ViewModels;

public class ChangeProfileViewModel : ProfileViewModelBase
{
    public static string UserName => ProfileViewModel.UserName;
    public override bool CanNavigateNext
    {
        get => false;
        protected set => throw new NotSupportedException();
    }
    public override bool CanNavigatePrevious => true;
}