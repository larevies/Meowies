using System;

namespace Meowies.ViewModels;

public class MovieViewModel : PageViewModelBase
{
    public override bool CanNavigateNext
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    public override bool CanNavigatePrevious => true;
    public override bool CanCat => true;
    public override bool CanSearch => true;
    public override bool CanRandom => true;
    public override bool CanFavourites => true;
    public override bool CanTrending => true;
}