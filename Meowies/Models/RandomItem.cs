using System;
using System.Collections.Generic;

namespace Meowies.Models;

    public class Budget
    {
    }

    public class Distributors
    {
        public string? distributor { get; set; }
        public string? distributorRelease { get; set; }
    }

    public class RandomExternalId
    {
        public string? kpHD { get; set; }
        public string? imdb { get; set; }
    }

    public class Fees
    {
        public World? world { get; set; }
        public Russia? russia { get; set; }
        public Usa? usa { get; set; }
    }

    public class Images
    {
        public int? framesCount { get; set; }
    }

    public class RandomName
    {
        public string name { get; set; }
    }

    public class Premiere
    {
        public DateTime world { get; set; }
    }

    public class RandomItem
    {
        public int id { get; set; }
        public string? type { get; set; }
        public RandomExternalId? externalId { get; set; }
        public string? name { get; set; }
        public Rating? rating { get; set; }
        public string? description { get; set; }
        public Votes? votes { get; set; }
        public Distributors? distributors { get; set; }
        public Premiere? premiere { get; set; }
        public object? slogan { get; set; }
        public int? year { get; set; }
        public Poster? poster { get; set; }
        public object? facts { get; set; }
        public List<Genre>? genres { get; set; }
        public List<Country>? countries { get; set; }
        public List<object>? seasonsInfo { get; set; }
        public List<Person>? persons { get; set; }
        public Images? images { get; set; }
        public List<object> lists { get; set; }
        public List<object> spokenLanguages { get; set; }
        public List<object> productionCompanies { get; set; }
        public int typeNumber { get; set; }
        public string? alternativeName { get; set; }
        public Budget budget { get; set; }
        public string? enName { get; set; }
        public List<RandomName> names { get; set; }
        public List<object> updateDates { get; set; }
        public Fees fees { get; set; }
        public DateTime updatedAt { get; set; }
        public int? movieLength { get; set; }
        public object? ratingMpaa { get; set; }
        public object? shortDescription { get; set; }
        public Technology? technology { get; set; }
        public bool? ticketsOnSale { get; set; }
        public List<object>? similarMovies { get; set; }
        public List<SequelsAndPrequel>? sequelsAndPrequels { get; set; }
        public int? ageRating { get; set; }
        public Backdrop backdrop { get; set; }
        public Logo logo { get; set; }
        public Watchability watchability { get; set; }
        public object top10 { get; set; }
        public object top250 { get; set; }
        public object status { get; set; }
        public object deletedAt { get; set; }
        public bool isSeries { get; set; }
        public object seriesLength { get; set; }
        public object totalSeriesLength { get; set; }
        public object networks { get; set; }
    }

    public class Russia
    {
    }

    public class SequelsAndPrequel
    {
        public int id { get; set; }
        public string name { get; set; }
        public object enName { get; set; }
        public string alternativeName { get; set; }
        public string type { get; set; }
        public Poster poster { get; set; }
        public int year { get; set; }
        public Rating rating { get; set; }
    }

    public class Technology
    {
        public bool hasImax { get; set; }
        public bool has3D { get; set; }
    }

    public class Usa
    {
    }

    public class Watchability
    {
        public object items { get; set; }
    }

    public class World
    {
    }