using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
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
            Movies.Clear();
            Actors.Clear();
            
            var name = HttpUtility.UrlEncode(SearchName);
            
            var task = JsonDeserializers.GetBmListAsync(
                Getters.GetMovieUrlByName(name));
            var item = await task!;
            foreach (var doc in item!.docs!)
            {
                if (doc.name == "")
                {
                    doc.name = "name is missing";
                }
                Movies.Add(doc); 
            }
            
            
            var taskActor = JsonDeserializers.GetAcListAsync(
                Getters.GetActorUrlByName(name));
            var itemActor = await taskActor!;
            foreach (var doc in itemActor!.docs!)
            {
                if (doc.name == "")
                {
                    doc.name = "name is missing";
                }
                Actors.Add(doc);
            }
            
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
    private ObservableCollection<MovieListDoc> _movies = new (new(10));
    public ObservableCollection<MovieListDoc> Movies
    {
        get => _movies;
        set
        {
            _movies = value;
            OnPropertyChanged(nameof(Movies));
        }
    }
    private ObservableCollection<ActorListDoc> _actors = new (new(10));
    public ObservableCollection<ActorListDoc> Actors
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
        var years = 0;
        try
        {
            var userAge = DateTime.Parse(SignInViewModel.CurrentUser.Birthday, CultureInfo.CurrentCulture);
            var now = DateTime.Now;
            var sub = now - userAge;
            years = sub.Days / 385;
        }
        catch (Exception e)
        {
            Console.WriteLine("Can't go there.");
            Console.WriteLine(e.Message);
            
        }
        finally
        {
            try
            {
                var task = JsonDeserializers.GetBmAsync(
                    Getters.GetMovieUrlById(id.ToString()));
                var item = await task!;

                if (item!.docs[0].ageRating > years)
                {
                    Console.WriteLine($"only {years} years. haha minor");
                }
                else
                {
                    Item = item.docs[0];
                    DownloadImage(Item.poster.url);
                    IsSearchVisible = false;
                    IsStartVisible = false;
                    IsResultVisible = false;
                    IsActorVisible = false;
                    IsMovieVisible = true;
                    IsGoBackVisible = true;
                    
                    using var context = new MeowiesContext();
                    context.Attach(SignInViewModel.CurrentUser); 
                    var queryable = context.Bookmarks.First(o => o.User == SignInViewModel.CurrentUser &&
                                                                 o.MovieId == Item.id);
                    if (queryable == null)
                    {
                        Bookmarked = "Bookmark me!";
                    }
                    else
                    {
                        Bookmarked = "Bookmarked";
                    }
                    Message = "";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
            Bookmarked = "Bookmark me!";
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
    
    private string _bookmarked = "Bookmark me!";
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

            if (Bookmarked == "Bookmark me!")
            {
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
            else if (Bookmarked == "Bookmarked")
            {
                Message = "Removed";
                var queryable = context.Bookmarks.First(o => o.User == SignInViewModel.CurrentUser &&
                                                                              o.MovieId == Item.id);
                context.Remove(queryable);
                context.SaveChanges();
                BookmarksViewModel.Bookmarks.Remove(Item);
                Bookmarked = "Bookmark me!";
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
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