using System;
using Avalonia.Controls;
using Meowies.Models;

namespace Meowies.ViewModels;

public abstract class ProfileViewModelBase : ViewModelBase
{
    public abstract bool CanNavigateNext { get; protected set; }
    public abstract bool CanNavigatePrevious { get; }
}