using System;
using System.Windows.Input;
using DynamicData;
using Meowies.Views;
using ReactiveUI;

namespace Meowies.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    public MainWindowViewModel()
    {
        _currentPage = _pages[0];

        var canNavNext = this.WhenAnyValue(x => x.CurrentPage.CanNavigateNext);
        var canNavPrev = this.WhenAnyValue(x => x.CurrentPage.CanNavigatePrevious);
        var canCat = this.WhenAnyValue(x => x.CurrentPage.CanCat);
        var canSearch = this.WhenAnyValue(x => x.CurrentPage.CanSearch);
        var canRandom = this.WhenAnyValue(x => x.CurrentPage.CanRandom);
        var canFavourites = this.WhenAnyValue(x => x.CurrentPage.CanFavourites);
        var canTrending = this.WhenAnyValue(x => x.CurrentPage.CanTrending);

        NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
        NavigatePreviousCommand = ReactiveCommand.Create(NavigatePrevious, canNavPrev);
        CatCommand = ReactiveCommand.Create(Cat, canCat);
        SearchCommand = ReactiveCommand.Create(Search, canSearch);
        RandomCommand = ReactiveCommand.Create(Random, canRandom);
        FavouritesCommand = ReactiveCommand.Create(Favourites, canFavourites);
        TrendingCommand = ReactiveCommand.Create(Trending, canTrending);
    }
    
    private string _next = "Sign up";
    private string _previous = "Sign in";
    public string Next
    {
        get => _next;
        set => this.RaiseAndSetIfChanged(ref _next, value);
    }
    public string Previous
    {
        get => _previous;
        set => this.RaiseAndSetIfChanged(ref _previous, value);
    }
    
    private readonly PageViewModelBase[] _pages = 
    { 
        new WelcomeViewModel(),
        new SignUpViewModel(),
        new SignInViewModel(),
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
    public ICommand NavigateNextCommand { get; }

    private void NavigateNext()
    {
        var index = _pages.IndexOf(CurrentPage) + 1;
        CurrentPage = _pages[index];
        if (CurrentPage == _pages[2])
        {
            Next = "Sign in";
            Previous = "Go back";
        }
        else if (CurrentPage == _pages[0])
        {
            Next = "Sign up";
            Previous = "Sign in";
        }
        else if (CurrentPage == _pages[1])
        {
            Next = "Sign up";
            Previous = "Go Back";
        }
    }
    public ICommand NavigatePreviousCommand { get; }

    private void NavigatePrevious()
    {
        int index;
        if (CurrentPage == _pages[0])
        {
            index = _pages.IndexOf(CurrentPage) + 2;
        }
        else
        {
            index = 0;
        }

        CurrentPage = _pages[index];
        if (CurrentPage == _pages[2])
        {
            Next = "Sign in";
            Previous = "Go back";
        }
        else if (CurrentPage == _pages[0])
        {
            Next = "Sign up";
            Previous = "Sign in";
        }
        else if (CurrentPage == _pages[1])
        {
            Next = "Sign up";
            Previous = "Go Back";
        }
    }

    public ICommand CatCommand { get; }
    private void Cat() { CurrentPage = _pages[3]; }
    
    public ICommand SearchCommand { get; }
    private void Search() { CurrentPage = _pages[5]; }
    
    public ICommand RandomCommand { get; }
    private void Random() { CurrentPage = _pages[5]; }
    
    public ICommand FavouritesCommand { get; }
    private void Favourites() { CurrentPage = _pages[4]; }
    
    public ICommand TrendingCommand { get; }
    private void Trending() { CurrentPage = _pages[6]; }
}