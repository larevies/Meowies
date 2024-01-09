namespace Meowies.Models;

public interface IUrlGettable
{
    public string Get(string name);
}

public class ActorUrlByNameGetter : IUrlGettable
{
    public string Get(string name)
    {
        return $"https://api.kinopoisk.dev/v1.4/person/search?page=1&limit=10&query={name}&token=41PANE7-0A44MD7-NRYZ232-8016VQY";
    }
}
public class MovieUrlByIdGetter : IUrlGettable
{
    public string Get(string id)
    {
        return $"https://api.kinopoisk.dev/v1.4/movie?page=1&limit=10&selectFields=id&selectFields=enName&selectFields=description&selectFields=type&selectFields=year&selectFields=rating&selectFields=ageRating&selectFields=votes&selectFields=movieLength&selectFields=genres&selectFields=countries&selectFields=poster&selectFields=alternativeName&selectFields=persons&id={id}&token=41PANE7-0A44MD7-NRYZ232-8016VQY";
    }
}

public class MovieUrlByNameGetter : IUrlGettable
{
    public string Get(string name)
    {
        return $"https://api.kinopoisk.dev/v1.4/movie/search?page=1&limit=10&query={name}&token=41PANE7-0A44MD7-NRYZ232-8016VQY";
    }
}