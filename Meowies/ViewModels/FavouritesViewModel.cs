using System.Collections.Generic;
using Meowies.Models;

namespace Meowies.ViewModels;

public class FavouritesViewModel : ViewModelBase
{
    public static List<MovieItemDoc> Bookmarks { get; set; } = new();
}