using System;
using System.IO;
using System.Net;
using System.Windows.Input;
using Meowies.Models;
using ReactiveUI;

namespace Meowies.ViewModels;

public class MovieViewModel : PageViewModelBase
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
        set => this.RaiseAndSetIfChanged(ref _bookmarked, value);
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
    public static string PosterUrl = "https://ih1.redbubble.net/image.4764410387.7815/bg,f8f8f8-flat,750x,075,f-pad,750x1000,f8f8f8.jpg";
    public static Avalonia.Media.Imaging.Bitmap Poster { get; set; } = null!;
    public static void DownloadImage(string url)
    {
        using WebClient client = new WebClient();
        client.DownloadDataAsync(new Uri(url));
        client.DownloadDataCompleted += DownloadComplete;
    }

    private static void DownloadComplete(object sender, DownloadDataCompletedEventArgs e)
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

    public override bool CanCat => true;
    public override bool CanSearch => true;
    public override bool CanRandom => true;
    public override bool CanFavourites => true;
    public override bool CanTrending => true;
}



// Bookmark.docs[0].poster.previewUrl
/*public string PosterUrl
{
    get => _posterUrl;
    set {
        this.RaiseAndSetIfChanged(ref _posterUrl, value);
        DownloadImage(PosterUrl);
    }
}*/
/*public Avalonia.Media.Imaging.Bitmap Poster
{
    get => _poster;
    set => this.RaiseAndSetIfChanged(ref _poster, value);
}*/