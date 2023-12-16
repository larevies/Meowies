using System;

namespace Meowies.ViewModels;

public class TrendingViewModel : PageViewModelBase
{
    public override bool CanCat => true;
    public override bool CanSearch => true;
    public override bool CanRandom => true;
    public override bool CanFavourites => true;
    public override bool CanTrending => true;
}