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
    }

    public string SearchResults { get; set; } = "Search results!";
    public ICommand SearchCommandA { get; }
    private async void SearchA()
    {
        try
        {
            var name = HttpUtility.UrlEncode(SearchName);
            
            var task = JSONDeserializers.GetBmListAsync(ApiQueries.MovieUrl(name));
            var item = await task!;
            foreach (var doc in item!.docs)
            { Bookmarks.Add(doc); }
            
            
            var taskActor = JSONDeserializers.GetAcListAsync(ApiQueries.ActorUrl(name));
            var itemActor = await taskActor!;
            foreach (var doc in itemActor!.docs)
            { Actors.Add(doc); }
            
            IsSearchVisible = true;
            IsResultVisible = true;
            IsMovieVisible = false;
            IsActorVisible = false;
            
        }
        catch (Exception e)
        {
            Console.WriteLine("Wrong");
            Console.WriteLine(e.Message);
        }
    }
    public string SearchName { get; set; } = null!;
    private List<MovieListDoc> _bookmarks = new(10);
    public List<MovieListDoc> Bookmarks
    {
        get => _bookmarks;
        set
        {
            _bookmarks = value;
            OnPropertyChanged(nameof(Bookmarks));
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
    private bool _isGoBackVisible;
    public bool IsGoBackVisible { 
        get => _isGoBackVisible;
        set
        {
            _isGoBackVisible = value;
            OnPropertyChanged(nameof(IsGoBackVisible));
        }
    }
    
    public async void BookmarkSearchSwitch(MovieListDoc a)
    {
        IsSearchVisible = false;
        IsResultVisible = false;
        IsMovieVisible = true;
        IsGoBackVisible = true;
        var task = JSONDeserializers.GetBmAsync(ApiQueries.IdMovieUrl(a.id.ToString()));
        var item = await task!;
        Item = item!.docs[0];
        DownloadImage(Item.poster.url);

    } 
    public async void ActorSearchSwitch(ActorListDoc a)
    {
        IsSearchVisible = false;
        IsResultVisible = false;
        IsActorVisible = true;
        IsGoBackVisible = true;
        var task = JSONDeserializers.GetAcAsync(ApiQueries.IdActorUrl(a.id.ToString()));
        var item = await task!;
        ActorItem = item!.docs[0];
        DownloadImage(ActorItem.photo);
    }
    
    public ICommand GoBackCommand { get; }
    public void GoBack()
    {
        IsSearchVisible = true;
        IsActorVisible = false;
        IsMovieVisible = false;
        IsGoBackVisible = false;
        IsResultVisible = false;
        Bookmarks.Clear();
        Actors.Clear();
        Item = new MovieItemDoc();
        ActorItem = new ActorItemDoc();
        Poster = null;
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
    public void AddToBookmarks()
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
    
    private Avalonia.Media.Imaging.Bitmap _poster;
    public Avalonia.Media.Imaging.Bitmap Poster
    {
        get => _poster;
        set
        {
            _poster = value;
            OnPropertyChanged(nameof(Poster));
        }
    }
    public void DownloadImage(string url)
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