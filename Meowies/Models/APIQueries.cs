namespace Meowies.Models;

public class ApiQueries
{
    public static string IdMovieUrl(string id)
    {
        return $"https://api.kinopoisk.dev/v1.4/movie?page=1&limit=10&selectFields=id&selectFields=enName&selectFields=description&selectFields=type&selectFields=year&selectFields=rating&selectFields=ageRating&selectFields=votes&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=poster&selectFields=alternativeName&selectFields=persons&id={id}&token={ApiToken.TOKEN}";
    }

    public static string MovieUrl(string name)
    {
        return $"https://api.kinopoisk.dev/v1.4/movie/search?page=1&limit=10&query={name}&token={ApiToken.TOKEN}";
    }

    public static string ActorUrl(string name)
    {
        return $"https://api.kinopoisk.dev/v1.4/person/search?page=1&limit=10&query={name}&token={ApiToken.TOKEN}";
    }

    public static string IdActorUrl(string id)
    {
        return $"https://api.kinopoisk.dev/v1.4/person?page=1&limit=10&selectFields=id&selectFields=name&selectFields=enName&selectFields=photo&selectFields=sex&selectFields=growth&selectFields=birthday&selectFields=age&selectFields=birthPlace&selectFields=countAwards&selectFields=profession&selectFields=facts&selectFields=movies&id={id}&token={ApiToken.TOKEN}";
    }

    public static string RandomUrl = $"https://api.kinopoisk.dev/v1.4/movie/random?page=1&limit=10&selectFields=id&selectFields=enName&selectFields=description&selectFields=type&selectFields=year&selectFields=rating&selectFields=ageRating&selectFields=votes&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=poster&selectFields=alternativeName&selectFields=persons&token={ApiToken.TOKEN}";

}