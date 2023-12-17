using System;
using System.Net.Http;
using System.Threading.Tasks;
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

    public string MovieUrl(string id)
    {
        return $"https://api.kinopoisk.dev/v1.4/movie?page=1&limit=10&selectFields=id&selectFields=enName&selectFields=description&selectFields=type&selectFields=year&selectFields=rating&selectFields=ageRating&selectFields=votes&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=poster&selectFields=alternativeName&selectFields=persons&id={id}&token=41PANE7-0A44MD7-NRYZ232-8016VQY";
    }

    public string MovieId { get; set; } = null!;

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

    private async void Search()
    {
        try
        {
            var task = GetBmListAsync(MovieUrl(MovieId));
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
    public ICommand RandomCommand { get; }

    private async void Random()
    {
        try
        {
            var rnd = new Random();
            int[] ids = {939785, 86621, 683999, 571892, 1211076}; 
            var index  = rnd.Next(0, 5);
            int id = ids[index];
            var task = GetBmListAsync(MovieUrl(id.ToString()));
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
            var task = GetBmListAsync(MovieUrl(id.ToString()));
            var item = await task;
            TrendingViewModel.Bookmark = item;
            CurrentPage = _pages[3];
        }
        catch (Exception)
        {
            CurrentPage = _pages[3];
        }
    }
    
    private static async Task<BookmarkItem?>? GetBmListAsync(string apiUrl)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var bmList = JsonConvert.DeserializeObject<BookmarkItem>(json);
            return bmList;
        }

        throw new Exception($"HTTP request failed with status code {response.StatusCode}");
    }
    
    // TODO how to do design for things from API (for example, we have different bookmarks for every user)?
    // TODO request datetime
    
}
