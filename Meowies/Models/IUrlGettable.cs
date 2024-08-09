namespace Meowies.Models;

public interface IUrlGettable
{
    public string Get(string name);
}

public class ActorUrlByIdGetter : IUrlGettable
{
    public string Get(string id)
    {
        return $"https://api.kinopoisk.dev/v1.4/person?page=1&limit=10&selectFields=id&selectFields=name&selectFields=enName&selectFields=photo&selectFields=sex&selectFields=growth&selectFields=birthday&selectFields=age&selectFields=birthPlace&selectFields=countAwards&selectFields=profession&selectFields=facts&selectFields=movies&id={id}&token={ApiToken.Token}";
    }
}

public class ActorUrlByNameGetter : IUrlGettable
{
    public string Get(string name)
    {
        return $"https://api.kinopoisk.dev/v1.4/person/search?page=1&limit=10&query={name}&token={ApiToken.Token}";
    }
}

public class MovieUrlByIdGetter : IUrlGettable
{
    public string Get(string id)
    {
        return $"https://api.kinopoisk.dev/v1.4/movie?page=1&limit=10&selectFields=id&selectFields=name&selectFields=enName&selectFields=description&selectFields=type&selectFields=year&selectFields=rating&selectFields=ageRating&selectFields=votes&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=poster&selectFields=alternativeName&selectFields=persons&id={id}&token={ApiToken.Token}";
    }
}

public class MovieUrlByNameGetter : IUrlGettable
{
    public string Get(string name)
    {
        return $"https://api.kinopoisk.dev/v1.4/movie/search?page=1&limit=10&query={name}&token={ApiToken.Token}";
    }
}

public class RandomMovieGetter : IUrlGettable
{
    public string Get(string name)
    {
        return $"https://api.kinopoisk.dev/v1.4/movie/random?page=1&limit=10&selectFields=id&selectFields=enName&selectFields=description&selectFields=type&selectFields=year&selectFields=rating&selectFields=ageRating&selectFields=votes&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=poster&selectFields=alternativeName&selectFields=persons&token={ApiToken.Token}";
    }
}