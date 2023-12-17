using System;
using Meowies.Models;

namespace Meowies.ViewModels;

public class TrendingViewModel : PageViewModelBase
{
    public static BookmarkItem Bookmark { get; set; } = null!;
    public override bool CanCat => true;
    public override bool CanSearch => true;
    public override bool CanRandom => true;
    public override bool CanFavourites => true;
    public override bool CanTrending => true;
}