using System;
using System.Collections.Generic;
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

    public static string MovieUrl(string name)
    {
        //return $"https://api.kinopoisk.dev/v1.4/movie?page=1&limit=10&selectFields=id&selectFields=enName&selectFields=description&selectFields=type&selectFields=year&selectFields=rating&selectFields=ageRating&selectFields=votes&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=poster&selectFields=alternativeName&selectFields=persons&id={id}&token=41PANE7-0A44MD7-NRYZ232-8016VQY";
        return $"https://api.kinopoisk.dev/v1.4/movie/search?page=1&limit=10&query={name}&token=41PANE7-0A44MD7-NRYZ232-8016VQY";
    }

    public static string ActorUrl(string name)
    {
        return $"https://api.kinopoisk.dev/v1.4/person/search?page=1&limit=10&query={name}&token=41PANE7-0A44MD7-NRYZ232-8016VQY";
    }

    // private string _randomUrl = "https://api.kinopoisk.dev/v1.4/movie/random?page=1&limit=10&selectFields=id&selectFields=enName&selectFields=description&selectFields=type&selectFields=year&selectFields=rating&selectFields=ageRating&selectFields=votes&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=poster&selectFields=alternativeName&selectFields=persons&token=41PANE7-0A44MD7-NRYZ232-8016VQY";
    public string SearchName { get; set; } = null!;

    public readonly PageViewModelBase[] _pages = 
    { 
        new ProfileViewModel(),
        new FavouritesViewModel(),
        new MovieViewModel(),
        new TrendingViewModel(), 
        new SearchViewModel()
    };
    
    private PageViewModelBase _currentPage;
    public PageViewModelBase CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public ICommand CatCommand { get; }
    private void Cat() { CurrentPage = _pages[0]; }
    
    public ICommand SearchCommand { get; }

    private async void Search()
    {
        try
        {
            var name = HttpUtility.UrlEncode(SearchName);
            
            var task = GetBmListAsync(MovieUrl(name));
            var item = await task!;
            foreach (var doc in item!.docs)
            {
                var newBookmark = new BookmarkList();

                newBookmark.docs = new List<Doc>(11);
                newBookmark.docs.Add(doc);
                newBookmark.total = 1;
                newBookmark.limit = 1;
                newBookmark.page = 1;
                newBookmark.pages = 1;

                SearchViewModel.Bookmarks.Add(newBookmark);
            }
            
            
            var taskActor = GetAcListAsync(ActorUrl(name));
            var itemActor = await taskActor!;
            foreach (var doc in itemActor!.docs)
            {
                var newActor = new ActorList();

                newActor.docs = new List<ActorDoc>(11);
                newActor.docs.Add(doc);
                newActor.total = 1;
                newActor.limit = 1;
                newActor.page = 1;
                newActor.pages = 1;

                SearchViewModel.Actors.Add(newActor);
            }
            
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


            //SearchViewModel.CurrentPage = _pages[0];
        }
        catch (Exception)
        {
            MovieViewModel.Message = "Wrong";
            CurrentPage = _pages[4];
        }
        //CurrentPage = _pages[2];
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
    
    public void Switch(Doc a)
    {
        MovieViewModel.MovieDoc = a;
        CurrentPage = _pages[2];
    }
}
