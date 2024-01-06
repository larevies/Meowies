using System.Collections.Generic;
using Meowies.Models;

namespace Meowies.ViewModels;

public class FavouritesViewModel : PageViewModelBase
{
    public static List<BookmarkItem> Bookmarks { get; set; } = new() { };
    public override bool CanCat => true;
    public override bool CanSearch => true;
    public override bool CanRandom => true;
    public override bool CanFavourites => true;
    public override bool CanTrending => true;
    
}