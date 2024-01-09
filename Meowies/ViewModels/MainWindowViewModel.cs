using System;
using System.Windows.Input;
using Meowies.Models;
using ReactiveUI;


namespace Meowies.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    public MainWindowViewModel()
    {
        
        var context = new MeowiesContext();
        context.Database.EnsureCreated();
        
        _currentPage = _pages[0];
        
        CatCommand = ReactiveCommand.Create(Cat);
        SearchCommand = ReactiveCommand.Create(Search);
        RandomCommand = ReactiveCommand.Create(Random);
        FavouritesCommand = ReactiveCommand.Create(Favourites);
        TrendingCommand = ReactiveCommand.Create(Trending);
    }
    public string SearchName { get; set; } = null!;

    private readonly ViewModelBase[] _pages = 
    { 
        new ProfileViewModel(),
        new FavouritesViewModel(),
        new MovieViewModel(),
        new TrendingViewModel(), 
        new SearchViewModel()
    };
    
    private ViewModelBase _currentPage;
    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set
        {
            _currentPage = value;
            OnPropertyChanged(nameof(CurrentPage));
        }
    }

    public ICommand CatCommand { get; }
    private void Cat() { CurrentPage = _pages[0]; }
    
    public ICommand SearchCommand { get; }

    private async void Search()
    {
        try { CurrentPage = _pages[4]; }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            CurrentPage = _pages[4];
        }
    }
    public ICommand RandomCommand { get; }

    private async void Random()
    {
        try
        {
            var rnd = new Random();
            int[] ids = {939785, 86621, 683999, 571892, 1211076}; 
            var index  = rnd.Next(0, 5);
            int id = ids[index];
            var task = JSONDeserializers.GetBmAsync(ApiQueries.IdMovieUrl(id.ToString()));
            var item = await task!;
            MovieViewModel.MovieBookmarkDoc = item!.docs[0];
            CurrentPage = _pages[2];
        }
        catch (Exception)
        {
            CurrentPage = _pages[2];
        }
    }
    
    public ICommand FavouritesCommand { get; }
    private void Favourites() { CurrentPage = _pages[1]; }
    
    public ICommand TrendingCommand { get; }

    private async void Trending()
    {
        try
        {
            var id = 1071383;
            var task = JSONDeserializers.GetBmAsync(ApiQueries.IdMovieUrl(id.ToString()));
            var item = await task!;
            TrendingViewModel.BookmarkDoc = item!.docs[0];
            CurrentPage = _pages[3];
        }
        catch (Exception)
        {
            CurrentPage = _pages[3];
        }
    }

}
