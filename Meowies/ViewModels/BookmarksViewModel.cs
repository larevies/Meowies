using System.Collections.Generic;
using Meowies.Models;

namespace Meowies.ViewModels;

public class BookmarksViewModel : ViewModelBase
{
    public static List<MovieItem> Bookmarks { get; set; } = new();
}