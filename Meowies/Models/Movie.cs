using System.Collections.Generic;
using System.Security.Cryptography;

namespace Meowies.Models;

public class Movie
{
    public string Name { get; set; }             // docs[0].enName
    public List<Genre> Genres { get; set; }      // docs[0].genres
    public List<Country> Countries { get; set; } // docs[0].countries
    public string Length { get; set; }           // docs[0].movieLength
    public string AgeRating { get; set; }        // docs[0].ageRating
    public string ReleaseYear { get; set; }      // docs[0].year
    public string Description { get; set; }      // docs[0].description
    public string Poster { get; set; }           // docs[0].poster.url
    public string KpRating { get; set; }         // docs[0].rating.kp
    public string NumberOfVotes { get; set; }    // docs[0].votes.kp
    public List<Person> Persons { get; set; }           // docs[0].
    public override string ToString()
    {
        return $"Name: {Name}, Email: {AgeRating}, Birthday: {Description}, Password: {ReleaseYear})";
    }
}