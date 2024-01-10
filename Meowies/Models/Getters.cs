namespace Meowies.Models;

public class Getters
{
    public static string GetMovieUrlById(string id)
    {
        MovieUrlByIdGetter getter = new MovieUrlByIdGetter();
        return getter.Get(id);
    }
    public static string GetMovieUrlByName(string name)
    {
        MovieUrlByNameGetter getter = new MovieUrlByNameGetter();
        return getter.Get(name);
    }
    public static string GetActorUrlById(string id)
    {
        ActorUrlByIdGetter getter = new ActorUrlByIdGetter();
        return getter.Get(id);
    }
    public static string GetActorUrlByName(string name)
    {
        ActorUrlByNameGetter getter = new ActorUrlByNameGetter();
        return getter.Get(name);
    }
}