using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Input;
using Meowies.Models;
using ReactiveUI;

namespace Meowies.ViewModels;

public class MovieViewModel : ViewModelBase
{
    public MovieViewModel()
    {
        DownloadImage(PosterUrl);
        AddToBookmarksCommand = ReactiveCommand.Create(AddToBookmarks);
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
                MovieId = Bookmark.docs[0].id
            };
            context.Bookmarks.Add(newBookmark);
            context.SaveChanges();
            Bookmarked = "Bookmarked";
        }
        catch(Exception)
        {
            Console.Write("аэыаээыэ. u are not logged in");
        }
    }
    public static string Message { get; set; } = "";
    public string PosterUrl = "https://ih1.redbubble.net/image.4764410387.7815/bg,f8f8f8-flat,750x,075,f-pad,750x1000,f8f8f8.jpg";

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
    public static BookmarkItem Bookmark { get; set; } = null!;
    public static BookmarkDoc MovieBookmarkDoc { get; set; } = null!;
}
