using System;
using System.ComponentModel;
using Meowies.Models;

namespace Meowies.ViewModels;

public class TrendingViewModel : ViewModelBase
{
    public static MovieItemDoc Bookmark { get; set; } = null!;
}