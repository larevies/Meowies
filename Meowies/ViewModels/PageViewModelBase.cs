namespace Meowies.ViewModels;

public abstract class PageViewModelBase : ViewModelBase
{
    public abstract bool CanCat { get; }
    public abstract bool CanSearch { get; }
    public abstract bool CanRandom { get; }
    public abstract bool CanFavourites { get; }
    public abstract bool CanTrending { get; }

}