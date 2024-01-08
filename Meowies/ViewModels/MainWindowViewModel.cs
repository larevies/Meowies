using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Meowies.Models;
using Newtonsoft.Json;
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

    public static string IdMovieUrl(string id)
    {
        return $"https://api.kinopoisk.dev/v1.4/movie?page=1&limit=10&selectFields=id&selectFields=enName&selectFields=description&selectFields=type&selectFields=year&selectFields=rating&selectFields=ageRating&selectFields=votes&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=poster&selectFields=alternativeName&selectFields=persons&id={id}&token=41PANE7-0A44MD7-NRYZ232-8016VQY";
    }

    public static string MovieUrl(string name)
    {
        return $"https://api.kinopoisk.dev/v1.4/movie/search?page=1&limit=10&query={name}&token=41PANE7-0A44MD7-NRYZ232-8016VQY";
    }

    private static string ActorUrl(string name)
    {
        return $"https://api.kinopoisk.dev/v1.4/person/search?page=1&limit=10&query={name}&token=41PANE7-0A44MD7-NRYZ232-8016VQY";
    }
    private string RandomUrl = "https://api.kinopoisk.dev/v1.4/movie/random?page=1&limit=10&selectFields=id&selectFields=enName&selectFields=description&selectFields=type&selectFields=year&selectFields=rating&selectFields=ageRating&selectFields=votes&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=poster&selectFields=alternativeName&selectFields=persons&token=41PANE7-0A44MD7-NRYZ232-8016VQY";
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
        try
        {
            //SearchViewModel.IsSearchVisible = true;
            //SearchViewModel.IsMovieVisible = false;
            //SearchViewModel.IsActorVisible = false;
            
            var name = HttpUtility.UrlEncode(SearchName);
            
            var task = GetBmListAsync(MovieUrl(name));
            var item = await task!;
            foreach (var doc in item!.docs)
            { SearchViewModel.Bookmarks.Add(doc); }
            
            
            var taskActor = GetAcListAsync(ActorUrl(name));
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
            var task = GetBmAsync(MovieUrl(id.ToString()));
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
            var task = GetBmAsync(MovieUrl(id.ToString()));
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
