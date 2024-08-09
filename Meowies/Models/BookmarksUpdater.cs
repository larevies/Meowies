using System;
using Meowies.ViewModels;

namespace Meowies.Models;

public class BookmarksUpdater
{
    public static void Add(User user, int movieId)
    {
       /* using var context = new MeowiesContext();
        context.Attach(user);
        var newBookmark = new Bookmark()
        {
            User = SignInViewModel.CurrentUser,
            MovieId = movieId
        };
        context.Bookmarks.Add(newBookmark);
        context.SaveChanges();*/
    }
}