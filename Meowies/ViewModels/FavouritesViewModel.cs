using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Meowies.ViewModels;

public class FavouritesViewModel : PageViewModelBase
{
    /*public ObservableCollection<Bookmark> Bookmarks { get; set; } = new();

    public FavouritesViewModel()
    {
        Bookmarks = new ObservableCollection<Bookmark>(new List<Bookmark>
        {
            new Bookmark("Moonrise Kingdom","drama", 7.7, "18+"),
            new Bookmark("Fantastic Mr.Fox","drama", 7.8, "18+")
        });
    }

    public override bool CanNavigateNext
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    public override bool CanNavigatePrevious => true;
    public override bool CanCat => true;
    public override bool CanSearch => true;
    public override bool CanRandom => true;
    public override bool CanFavourites => true;
    public override bool CanTrending => true;
}

public class Bookmark
{
    public string Title { get; set; }
    public string Genres { get; set; }
    public double Rating { get; set; }
    public string AgeRating { get; set; }

    public Bookmark(string title, string genres, double rating, string ageRating)
    {
        Title = title;
        Genres = genres;
        Rating = rating;
        AgeRating = ageRating;
    }*/
    public override bool CanNavigateNext
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    public override bool CanNavigatePrevious => true;
    public override bool CanCat => true;
    public override bool CanSearch => true;
    public override bool CanRandom => true;
    public override bool CanFavourites => true;
    public override bool CanTrending => true;
}