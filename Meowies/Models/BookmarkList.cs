using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meowies.Models;

public class Backdrop
{
    public string url { get; set; }
    public string previewUrl { get; set; }
}

public class Doc
{
    public int id { get; set; }
    public string name { get; set; }
    public string alternativeName { get; set; }
    public string enName { get; set; }
    public string type { get; set; }
    public int year { get; set; }
    public string description { get; set; }
    public string shortDescription { get; set; }
    public int movieLength { get; set; }
    public bool isSeries { get; set; }
    public bool ticketsOnSale { get; set; }
    public object totalSeriesLength { get; set; }
    public int? seriesLength { get; set; }
    public string ratingMpaa { get; set; }
    public int? ageRating { get; set; }
    public object top10 { get; set; }
    public int? top250 { get; set; }
    public int typeNumber { get; set; }
    public string status { get; set; }
    public List<Name> names { get; set; }
    public ExternalId externalId { get; set; }
    public Logo logo { get; set; }
    public Poster poster { get; set; }
    public Backdrop backdrop { get; set; }
    public Rating rating { get; set; }
    public Votes votes { get; set; }
    public List<Genre> genres { get; set; }
    public List<Country> countries { get; set; }
    public List<ReleaseYear> releaseYears { get; set; }
}

public class ExternalId
{
    public int tmdb { get; set; }
    public string imdb { get; set; }
    public object kpHD { get; set; }
}


public class Logo
{
    public string url { get; set; }
}

public class Name
{
    public string name { get; set; }
    public string language { get; set; }
    public string type { get; set; }

    [JsonProperty("$set")]
    public Set set { get; set; }
}


public class ReleaseYear
{
    public int start { get; set; }
    public int? end { get; set; }
}

public class BookmarkList
{
    public List<Doc> docs { get; set; }
    public int total { get; set; }
    public int limit { get; set; }
    public int page { get; set; }
    public int pages { get; set; }
}

public class Set
{
    public string language { get; set; }
    public string type { get; set; }
}

