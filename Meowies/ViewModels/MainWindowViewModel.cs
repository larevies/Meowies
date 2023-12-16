using System;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using Flurl.Http;
using Meowies.Views;
using ReactiveUI;

namespace Meowies.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    public MainWindowViewModel()
    {
        
        //using var context = new MeowiesContext();
        
        _currentPage = _pages[0];
        
        var canCat = this.WhenAnyValue(x => x.CurrentPage.CanCat);
        var canSearch = this.WhenAnyValue(x => x.CurrentPage.CanSearch);
        var canRandom = this.WhenAnyValue(x => x.CurrentPage.CanRandom);
        var canFavourites = this.WhenAnyValue(x => x.CurrentPage.CanFavourites);
        var canTrending = this.WhenAnyValue(x => x.CurrentPage.CanTrending);
        
        CatCommand = ReactiveCommand.Create(Cat, canCat);
        SearchCommand = ReactiveCommand.Create(Search, canSearch);
        RandomCommand = ReactiveCommand.Create(Random, canRandom);
        FavouritesCommand = ReactiveCommand.Create(Favourites, canFavourites);
        TrendingCommand = ReactiveCommand.Create(Trending, canTrending);
    }
    
    private readonly PageViewModelBase[] _pages = 
    { 
        new ProfileViewModel(),
        new FavouritesViewModel(),
        new MovieViewModel(),
        new TrendingViewModel()
    };
    
    private PageViewModelBase _currentPage;
    public PageViewModelBase CurrentPage
    {
        get => _currentPage;
        private set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public ICommand CatCommand { get; }
    private void Cat() { CurrentPage = _pages[0]; }
    
    public ICommand SearchCommand { get; }
    private void Search() { CurrentPage = _pages[2]; }
    
    public ICommand RandomCommand { get; }
    private void Random() { CurrentPage = _pages[2]; }
    
    public ICommand FavouritesCommand { get; }
    private void Favourites() { CurrentPage = _pages[1]; }
    
    public ICommand TrendingCommand { get; }
    private void Trending() { CurrentPage = _pages[3]; }
    
    // TODO how to connect users with app?
    // TODO how to get GET requests from API?
    // TODO how to do design for things from API (for example, we have different bookmarks for every user)?
    // TODO request datetime
    
}