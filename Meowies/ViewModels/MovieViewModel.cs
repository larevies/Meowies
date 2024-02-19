using System;
using System.Globalization;
using System.IO;
using System.Linq;
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
    public ICommand FindAMovieCommand { get; }
    private async void FindAMovie()
    {
        try
        {
            /***
             * next movies don't cause troubles
             */
            
            var rnd = new Random();
            var years = 0;
            try
            {
                var userAge = DateTime.Parse(SignInViewModel.CurrentUser.Birthday, CultureInfo.CurrentCulture);
                var now = DateTime.Now;
                var sub = now - userAge;
                years = sub.Days / 385;
                int[] authorized = {46483, 2360, 279102, 45319, 89514, 512883, 706655, 493208, 458, 42326, 256124, 707,
                    95231, 61249, 7908, 251733, 505898, 401152, 683999, 571892, 939785, 86621, 1143242, 535341, 4646634, 
                    586397, 342, 9691, 819101, 1047883, 2717, 394, 775276, 280172, 775273, 645118, 920265, 6006, 837530,
                    1015471, 401152, 540, 591929, 679486, 251733, 409424, 447301, 263531, 61237, 476, 538225, 505851};
                var rand = rnd.Next(0, authorized.Length);
                var id = authorized[rand];
                Message = "";
                var task = JsonDeserializers.GetBmAsync
                    (Getters.GetMovieUrlById(id.ToString()));
                var item = await task!;
                if (item!.docs[0].ageRating > years)
                {
                    FindAMovie();
                }
                else
                {
                    Item = item!.docs[0];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't go there.");
                Console.WriteLine(e.Message);
                int[] unauthorized = {46483, 2360, 279102, 45319, 89514, 512883, 706655, 493208, 458, 42326, 256124, 707,
                    95231, 61249, 7908};
                var rand = rnd.Next(0, unauthorized.Length);
                var id = unauthorized[rand];

                try
                {
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
                }
                catch (Exception a)
                {
                    Console.WriteLine(a.Message);
                }
                finally
                {
                    Message = "";
                    var task = JsonDeserializers.GetBmAsync
                        (Getters.GetMovieUrlById(id.ToString()));
                    var item = await task!;
                    Item = item!.docs[0];
                }
            }
            
            /****
             * next code CAUSES problems KILLS our api tokens
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
            
            DownloadImage(Item.poster.url);
            
        }
        catch (Exception e)
        {
            Console.WriteLine("Can't go there.");
            Console.WriteLine(e.Message);
        }
        IsMovieVisible = true;
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
