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

        //UpdateViewCommand = new UpdateViewCommand(this);
        
        CatCommand = ReactiveCommand.Create(Cat);
        SearchCommand = ReactiveCommand.Create(Search);
        RandomCommand = ReactiveCommand.Create(Random);
        BookmarksCommand = ReactiveCommand.Create(Bookmarks);
        TrendingCommand = ReactiveCommand.Create(Trending);
    }
    
    //public ICommand UpdateViewCommand { get; set; }

    /*private ViewModelBase _selectedViewModel = new MainWindowViewModel();

    public ViewModelBase SelectedViewModel
    {
        get => _selectedViewModel;
        set => _selectedViewModel = value ?? throw new ArgumentNullException(nameof(value));
    }*/

    private readonly ViewModelBase[] _pages = 
    { 
        new ProfileViewModel(),
        new SearchViewModel(),
        new MovieViewModel(),
        new BookmarksViewModel(),
        new TrendingViewModel()
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
    private void Search() { CurrentPage = _pages[1]; }
    
    
    public ICommand RandomCommand { get; }
    private void Random() { CurrentPage = _pages[2]; }
    
    
    public ICommand BookmarksCommand { get; }
    private void Bookmarks() { CurrentPage = _pages[3]; }


    public ICommand TrendingCommand { get; }
    private async void Trending()
    {
        try
        {
            var id = 1071383;
            var task = JsonDeserializers.GetBmAsync
                (Getters.GetMovieUrlById(id.ToString()));
            var item = await task;

            TrendingViewModel.Bookmark = item.docs[0];
            CurrentPage = _pages[4];
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            CurrentPage = _pages[4];
        }
    }
}
