using System.Collections.Generic;
using Meowies.Models;

namespace Meowies.ViewModels;

public class SearchViewModel : PageViewModelBase
{
    public static List<BookmarkList> Bookmarks { get; set; } = new() { };
    public static List<ActorList> Actors { get; set; } = new() { };
    public override bool CanCat => true;
    public override bool CanSearch => true;
    public override bool CanRandom => true;
    public override bool CanFavourites => true;
    public override bool CanTrending => true;
}