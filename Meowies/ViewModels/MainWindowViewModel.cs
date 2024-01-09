using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Meowies.Commands;
using Meowies.Models;
using Newtonsoft.Json;
using ReactiveUI;

namespace Meowies.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand UpdateViewCommand { get; set; }

    public MainWindowViewModel()
    {
        
        var context = new MeowiesContext();
        context.Database.EnsureCreated();
        
        _currentPage = _pages[0];

        UpdateViewCommand = new UpdateViewCommand(this);
        
        CatCommand = ReactiveCommand.Create(Cat);
        SearchCommand = ReactiveCommand.Create(Search);
        RandomCommand = ReactiveCommand.Create(Random);
        FavouritesCommand = ReactiveCommand.Create(Favourites);
        TrendingCommand = ReactiveCommand.Create(Trending);
    }

    private ViewModelBase _selectedViewModel = new MainWindowViewModel();

    public ViewModelBase SelectedViewModel
    {
        get => _selectedViewModel;
        set => _selectedViewModel = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static string GetMovieUrlById(string id)
    {
        MovieUrlByIdGetter getter = new MovieUrlByIdGetter();
        return getter.Get(id);
    }

    public static string GetMovieUrlByName(string name)
    {
        MovieUrlByNameGetter getter = new MovieUrlByNameGetter();
        return getter.Get(name);
    }

    private static string GetActorUrlByName(string name)
    {
        ActorUrlByNameGetter getter = new ActorUrlByNameGetter();
        return getter.Get(name);
    }
    private string RandomUrl = "https://api.kinopoisk.dev/v1.4/movie/random?page=1&limit=10&selectFields=id&selectFields=enName&selectFields=description&selectFields=type&selectFields=year&selectFields=rating&selectFields=ageRating&selectFields=votes&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=poster&selectFields=alternativeName&selectFields=persons&token=41PANE7-0A44MD7-NRYZ232-8016VQY";
    public string SearchName { get; set; } = null!;

    private readonly ViewModelBase[] _pages = 
    { 
        new ProfileViewModel(),
        new BookmarksViewModel(),
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
        try
        {
            //SearchViewModel.IsSearchVisible = true;
            //SearchViewModel.IsMovieVisible = false;
            //SearchViewModel.IsActorVisible = false;
            
            var name = HttpUtility.UrlEncode(SearchName);
            
            var task = GetBmListAsync(GetMovieUrlByName(name));
            var item = await task!;
            foreach (var doc in item!.docs)
            { SearchViewModel.Bookmarks.Add(doc); }
            
            
            var taskActor = GetAcListAsync(GetActorUrlByName(name));
            var itemActor = await taskActor!;
            foreach (var doc in itemActor!.docs)
            { SearchViewModel.Actors.Add(doc); }
            
            //MovieViewModel.Bookmark = item;
            //MovieViewModel.PosterUrl = item.docs[0].poster.previewUrl;
            //MovieViewModel.DownloadImage(MovieViewModel.PosterUrl);
            //MovieViewModel.Message = "";
            
            /*var task = GetBmListAsync(MovieUrl("251733"));
            var item = await task;
            MovieViewModel.Bookmark = item;
            //MovieViewModel.PosterUrl = item.docs[0].poster.previewUrl;
            //MovieViewModel.DownloadImage(MovieViewModel.PosterUrl);
            MovieViewModel.Message = "";*/
            
            CurrentPage = _pages[4];
            
        }
        catch (Exception)
        {
            MovieViewModel.Message = "Wrong";
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
            var task = GetBmAsync(GetMovieUrlByName(id.ToString()));
            var item = await task;
            MovieViewModel.Bookmark = item;
            //MovieViewModel.PosterUrl = item.docs[0].poster.previewUrl;
            //MovieViewModel.DownloadImage(MovieViewModel.PosterUrl);
            MovieViewModel.Message = "";
            CurrentPage = _pages[2];
        }
        catch (Exception)
        {
            MovieViewModel.Message = "Wrong";
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
            var task = GetBmAsync(GetMovieUrlByName(id.ToString()));
            var item = await task;
            TrendingViewModel.Bookmark = item;
            CurrentPage = _pages[3];
        }
        catch (Exception)
        {
            CurrentPage = _pages[3];
        }
    }
    
    public static async Task<BookmarkItem?>? GetBmAsync(string apiUrl)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var bm = JsonConvert.DeserializeObject<BookmarkItem>(json);
            return bm;
        }

        throw new Exception($"HTTP request failed with status code {response.StatusCode}");
    }
    
    public static async Task<BookmarkList?>? GetBmListAsync(string apiUrl)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var bmList = JsonConvert.DeserializeObject<BookmarkList>(json);
            return bmList;
        }

        throw new Exception($"HTTP request failed with status code {response.StatusCode}");
    }
    
    
    
    public static async Task<ActorList?>? GetAcListAsync(string apiUrl)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var acList = JsonConvert.DeserializeObject<ActorList>(json);
            return acList;
        }

        throw new Exception($"HTTP request failed with status code {response.StatusCode}");
    }
    
}
