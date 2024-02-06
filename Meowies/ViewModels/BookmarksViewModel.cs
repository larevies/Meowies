using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Meowies.Models;
using ReactiveUI;

namespace Meowies.ViewModels;

public class BookmarksViewModel : ViewModelBase
{
    public static List<MovieItemDoc> Bookmarks { get; set; }

    private string _refresh = "";
    public string Refresh
    {
        get => _refresh;
        set
        {
            _refresh = value;
            OnPropertyChanged(nameof(Refresh));
        }
    }
    
    public void Delete(MovieItemDoc a)
    {
        MeowiesContext context = new MeowiesContext();
        
        var queryable = context.Bookmarks
            .FirstOrDefault(x => x.MovieId == a.id);
        
        var itemToDelete = Bookmarks.Single
            (x => x.id == a.id);
        
        a.IsButtonVisible = false;
        context.Bookmarks.Remove(queryable!);
        context.SaveChanges();
        
        
        List<MovieItemDoc> newBookmarks = Bookmarks;
        newBookmarks.Remove(itemToDelete);
        
        // Bookmarks.Remove(itemToDelete);
        Bookmarks = newBookmarks;
        
        // its static so it doesnt change
        
        
    }
}