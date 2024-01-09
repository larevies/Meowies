using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Input;
using Meowies.Models;
using ReactiveUI;

namespace Meowies.ViewModels;

public class SearchViewModel : ViewModelBase
{
    public SearchViewModel()
    {
        AddToBookmarksCommand = ReactiveCommand.Create(AddToBookmarks);
    }

    private BookmarkDoc _bookmarkDocA = new();
    public BookmarkDoc BookmarkDocA
    {
        get => _bookmarkDocA;
        set
        {
            _bookmarkDocA = value;
            OnPropertyChanged(nameof(BookmarkDocA));
        }
    }
    public static List<BookmarkDoc> Bookmarks { get; set; } = new();
    public static List<ActorDoc> Actors { get; set; } = new();
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
    
    public void BookmarkSearchSwitch(BookmarkDoc a)
    {
        IsSearchVisible = false;
        IsMovieVisible = true;
        BookmarkDocA = a;
        DownloadImage(BookmarkDocA.poster.url);
        MovieViewModel.MovieBookmarkDoc = a;
        Console.Write("bkmk\n");

    } 
    public void ActorSearchSwitch(BookmarkDoc a)
    {
        IsSearchVisible = false;
        IsActorVisible = true;
        MovieViewModel.MovieBookmarkDoc = a;
        Console.Write("act");
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
        // User user = SignInViewModel.CurrentUser;
        try
        {
            // BookmarksUpdater.Add(user, BookmarkDocA.id);
            using var context = new MeowiesContext();
            context.Attach(SignInViewModel.CurrentUser);
            var newBookmark = new Bookmark()
            {
                User = SignInViewModel.CurrentUser,
                MovieId = BookmarkDocA.id
            };
            context.Bookmarks.Add(newBookmark);
            context.SaveChanges();
            Bookmarked = "Bookmarked";
        }
        catch (Exception)
        {
            Console.Write("аэыаээыэ. u are not logged in");
            
        }
    }
    public static string Message { get; set; } = "";
    
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