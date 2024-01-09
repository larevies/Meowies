using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace Meowies.Models;

public class Country
{
    public string name { get; set; }
}

public class MovieItemDoc
{
    public Rating rating { get; set; }
    public Votes votes { get; set; }
    public int? movieLength { get; set; }
    public int id { get; set; }
    public string type { get; set; }
    public string description { get; set; }
    public int year { get; set; }
    public Poster poster { get; set; }
    public List<Genre> genres { get; set; }
    public List<Country> countries { get; set; }
    public List<Person> persons { get; set; }
    public string alternativeName { get; set; }
    public string enName { get; set; }
    public int? ageRating { get; set; }
    
}
public class Person
{
    public int id { get; set; }
    public string photo { get; set; }
    public string name { get; set; }
    public string enName { get; set; }
    public string description { get; set; }
    public string profession { get; set; }
    public string enProfession { get; set; }
}
public class Genre
{
    public string name { get; set; }
}

public class Poster : INotifyPropertyChanged
{
    private string _url = "";
    public string url
    {
        get => _url;
        set
        {
            _url = value;
            //DownloadImage(url);
            //OnPropertyChanged(nameof(url));
        }
    }
    public string previewUrl { get; set; }
    
    private Avalonia.Media.Imaging.Bitmap _thePoster;
    public Avalonia.Media.Imaging.Bitmap ThePoster
    {
        get => _thePoster;
        set
        {
            _thePoster = value;
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
            ThePoster = image;
        }
        catch (Exception) { ThePoster = null!; }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class Rating
{
    public double kp { get; set; }
    public double imdb { get; set; }
    public double filmCritics { get; set; }
    public double russianFilmCritics { get; set; }
    public object await { get; set; }
}

public class MovieItem
{
    public List<MovieItemDoc> docs { get; set; }
    public int total { get; set; }
    public int limit { get; set; }
    public int page { get; set; }
    public int pages { get; set; }
}

public class Votes
{
    public int kp { get; set; }
    public int imdb { get; set; }
    public int filmCritics { get; set; }
    public int russianFilmCritics { get; set; }
    public int await { get; set; }
}
