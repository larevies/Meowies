using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Windows.Input;
using Meowies.Models;
using ReactiveUI;

namespace Meowies.ViewModels;

public class SearchViewModel : ViewModelBase
{
    public SearchViewModel()
    {
        AddToBookmarksCommand = ReactiveCommand.Create(AddToBookmarks);
        SearchCommandA = ReactiveCommand.Create(SearchA);
        GoBackCommand = ReactiveCommand.Create(GoBack);
        
        // var UserBookmarks = SearchResults;

    }
    
    public string SearchResults { get; set; } = "Search results!";
    public ICommand SearchCommandA { get; }
    private async void SearchA()
    {
        try
        {
            Movies.Clear();
            Actors.Clear();
            
            var name = HttpUtility.UrlEncode(SearchName);
            
            var task = JsonDeserializers.GetBmListAsync(
                Getters.GetMovieUrlByName(name));
            var item = await task!;
            foreach (var doc in item!.docs!)
            { Movies.Add(doc); }
            
            
            var taskActor = JsonDeserializers.GetAcListAsync(
                Getters.GetActorUrlByName(name));
            var itemActor = await taskActor!;
            foreach (var doc in itemActor!.docs!)
            { Actors.Add(doc); }
            
            IsSearchVisible = true;
            IsStartVisible = false;
            IsResultVisible = true;
            IsMovieVisible = false;
            IsActorVisible = false;
            IsGoBackVisible = false;
            Message = "";

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    public string SearchName { get; set; } = null!;
    private List<MovieListDoc> _movies = new(10);
    public List<MovieListDoc> Movies
    {
        get => _movies;
        set
        {
            _movies = value;
            OnPropertyChanged(nameof(Movies));
        }
    }
    private List<ActorListDoc> _actors = new(10);
    public List<ActorListDoc> Actors
    {
        get => _actors;
        set
        {
            _actors = value;
            OnPropertyChanged(nameof(Actors));
        }
    }
    private MovieItemDoc _item = new();
    public MovieItemDoc Item
    {
        get => _item;
        set
        {
            _item = value;
            OnPropertyChanged(nameof(Item));
        }
    }
    private ActorItemDoc _actorItem = new();
    public ActorItemDoc ActorItem
    {
        get => _actorItem;
        set
        {
            _actorItem = value;
            OnPropertyChanged(nameof(ActorItem));
        }
    }
    private bool _isSearchVisible = true;
    public bool IsSearchVisible
    {
        get => _isSearchVisible;
        set
        {
            _isSearchVisible = value;
            OnPropertyChanged(nameof(IsSearchVisible));
        }
    }

    private bool _isMovieVisible;
    public bool IsMovieVisible { 
        get => _isMovieVisible;
        set
        {
            _isMovieVisible = value;
            OnPropertyChanged(nameof(IsMovieVisible));
        }
    } 
    private bool _isActorVisible;
    public bool IsActorVisible { 
        get => _isActorVisible;
        set
        {
            _isActorVisible = value;
            OnPropertyChanged(nameof(IsActorVisible));
        }
    } 
    private bool _isResultVisible;
    public bool IsResultVisible { 
        get => _isResultVisible;
        set
        {
            _isResultVisible = value;
            OnPropertyChanged(nameof(IsResultVisible));
        }
    }
    private bool _isStartVisible = true;
    public bool IsStartVisible { 
        get => _isStartVisible;
        set
        {
            _isStartVisible = value;
            OnPropertyChanged(nameof(IsStartVisible));
        }
    }
    private bool _isGoBackVisible;
    public bool IsGoBackVisible { 
        get => _isGoBackVisible;
        set
        {
            _isGoBackVisible = value;
            OnPropertyChanged(nameof(IsGoBackVisible));
        }
    }
    
    public async void MovieSearchSwitch(int id)
    {
        try
        {
            IsSearchVisible = false;
            IsStartVisible = false;
            IsResultVisible = false;
            IsActorVisible = false;
            IsMovieVisible = true;
            IsGoBackVisible = true;
            Message = "";
            var task = JsonDeserializers.GetBmAsync(
                Getters.GetMovieUrlById(id.ToString()));
            var item = await task!;
            Item = item!.docs[0];
            DownloadImage(Item.poster.url);
        }
        catch (Exception e)
        {
            Console.WriteLine("Can't go there.");
            Console.WriteLine(e.Message);
        }
    } 
    public async void ActorSearchSwitch(int id)
    {
        try {
            IsSearchVisible = false;
            IsStartVisible = false;
            IsResultVisible = false;
            IsMovieVisible = false;
            IsActorVisible = true;
            IsGoBackVisible = true;
            Message = "";
            var task = JsonDeserializers.GetAcAsync(
                Getters.GetActorUrlById(id.ToString()));
            var item = await task!;
            ActorItem = item!.docs![0];
            DownloadImage(ActorItem.photo!);
        }
        catch (Exception e)
        {
            Console.WriteLine("Can't go there.");
            Console.WriteLine(e.Message);
        }
    }
    
    public ICommand GoBackCommand { get; }
    private void GoBack()
    {
        try {
            IsSearchVisible = true;
            IsStartVisible = true;
            IsActorVisible = false;
            IsMovieVisible = false;
            IsGoBackVisible = false;
            IsResultVisible = false;
            Message = "";
            Bookmarked = "Bookmark me";
            Movies = new();
            Actors = new();
            Item = new();
            ActorItem = new();
            Poster = null!;
        }
        catch (Exception e)
        {
            Console.WriteLine("Can't go there.");
            Console.WriteLine(e.Message);
        }
    }
    
    private string _bookmarked = "Bookmark me";
    public string Bookmarked
    {
        get => _bookmarked;
        set
        {
            _bookmarked = value;
            OnPropertyChanged(nameof(Bookmarked));
        }
    }

    public ICommand AddToBookmarksCommand { get; }
    private void AddToBookmarks()
    {
        try
        {
            using var context = new MeowiesContext();
            context.Attach(SignInViewModel.CurrentUser);
            var newBookmark = new Bookmark()
            {
                User = SignInViewModel.CurrentUser,
                MovieId = Item.id
            };
            context.Bookmarks.Add(newBookmark);
            context.SaveChanges();
            BookmarksViewModel.Bookmarks.Add(Item);
            Bookmarked = "Bookmarked";
        }
        catch(Exception)
        {
            Message = "you are not logged in.\nlog in to save movies!";
        }
    }
    private string _message = "";
    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }
    
    private Avalonia.Media.Imaging.Bitmap _poster = null!;
    public Avalonia.Media.Imaging.Bitmap Poster
    {
        get => _poster;
        set
        {
            _poster = value;
            OnPropertyChanged(nameof(Poster));
        }
    }
    private void DownloadImage(string url)
    {
        using WebClient client = new WebClient();
        client.DownloadDataAsync(new Uri(url));
        client.DownloadDataCompleted += DownloadComplete;
    }

    private void DownloadComplete(object sender, DownloadDataCompletedEventArgs e)
    {
        try
        {
            byte[] bytes = e.Result;
            Stream stream = new MemoryStream(bytes);
            var image = new Avalonia.Media.Imaging.Bitmap(stream);
            Poster = image;
        }
        catch (Exception) { Poster = null!; }
    }
}