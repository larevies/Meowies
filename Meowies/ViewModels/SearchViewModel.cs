using System;
using System.Collections.Generic;
using System.Windows.Input;
using Meowies.Models;
using ReactiveUI;
using static Meowies.ViewModels.MainWindowViewModel;

namespace Meowies.ViewModels;

public class SearchViewModel : PageViewModelBase
{
    /*public SearchViewModel() {
        SwitchCommand = ReactiveCommand.Create(Switch);
    }
    private string _switchToMovie = "doesnt work";
    public string SwitchToMovie
    {
        get => _switchToMovie;
        set => this.RaiseAndSetIfChanged(ref _switchToMovie, value);
    }
    public ICommand SwitchCommand { get; }
    void Switch()
    {
        //MovieViewModel.Bookmark = SearchViewModel.Bookmarks[0];
        SwitchToMovie = "worked";
    }*/

    /*public SearchViewModel()
    {
        CurrentPage = _pages[0];
    }
    private readonly PageViewModelBase[] _pages = 
    { 
        new SearchViewModel(),
        new MovieViewModel()
    };
    
    private PageViewModelBase _currentPage;
    public PageViewModelBase CurrentPage
    {
        get => _currentPage;
        private set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }*/
    
    
    
    //private MainWindowViewModel m = new MainWindowViewModel();
    public static List<BookmarkList> Bookmarks { get; set; } = new() { };
    public static List<ActorList> Actors { get; set; } = new() { };
    public override bool CanCat => true;
    public override bool CanSearch => true;
    public override bool CanRandom => true;
    public override bool CanFavourites => true;
    public override bool CanTrending => true;
    private readonly MainWindowViewModel _window = null!;
    public void SearchSwitch(Doc a)
    {
        MovieViewModel.MovieDoc = a;
        _window.Switch(a);
    }
}