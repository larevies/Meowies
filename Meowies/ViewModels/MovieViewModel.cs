using System;
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
        AddToBookmarksCommand = ReactiveCommand.Create(AddToBookmarks);
        FindAMovieCommand = ReactiveCommand.Create(FindAMovie);
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
    private bool _isMovieVisible;
    public bool IsMovieVisible { 
        get => _isMovieVisible;
        set
        {
            _isMovieVisible = value;
            OnPropertyChanged(nameof(IsMovieVisible));
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
    public ICommand FindAMovieCommand { get; }
    private async void FindAMovie()
    {
        try
        {
            /***
             * next movies don't cause troubles
             */
            
            var rnd = new Random();
            int[] b = {251733, 505898, 401152, 683999, 571892, 939785, 86621, 1143242, 535341, 4646634, 
                586397, 342, 9691, 819101, 1047883, 2717, 394};
            var rand = rnd.Next(0, b.Length);
            var id = b[rand];
            Message = "";
            var task = JSONDeserializers.GetBmAsync(ApiQueries.IdMovieUrl(id.ToString()));
            var item = await task!;
            Item = item!.docs[0];
            
            /****
             * next code CAUSES problems though should better use it not above
             */
            
            /*var task = JSONDeserializers.GetRndAsync(ApiQueries.RandomUrl);
            var item = await task!;
            MovieItemDoc a = new()
            {
                rating = item!.rating!,
                votes = item.votes!,
                movieLength = item.movieLength,
                id = item.id,
                type = item.type!,
                description = item.description!,
                year = item.year,
                poster = item.poster!,
                genres = item.genres!,
                countries = item.countries!,
                persons = item.persons!,
                alternativeName = item.alternativeName!,
                enName = item.enName!,
                ageRating = item.ageRating
            }; */
            
            DownloadImage(Item.poster!.url);
            
        }
        catch (Exception e)
        {
            Console.WriteLine("Can't go there.");
            Console.WriteLine(e.Message);
            //FindAMovie();
            
        }
        IsMovieVisible = true;
    }
    public ICommand AddToBookmarksCommand { get; }
    public void AddToBookmarks()
    {
        try
        {
            using var context = new MeowiesContext();
            context.Attach(SignInViewModel.CurrentUser);
            var newBookmark = new Bookmark
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
