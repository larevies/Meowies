using System;
using System.Collections.Generic;

namespace Meowies.Models;

public class BirthPlace
{
    public string? value { get; set; }
}

public class ActorItemDoc
{
    public int id { get; set; }
    public int? age { get; set; }
    public List<BirthPlace>? birthPlace { get; set; }
    public DateTime? birthday { get; set; }
    public int? countAwards { get; set; }
    public string? enName { get; set; }
    public List<Fact>? facts { get; set; }
    public int? growth { get; set; }
    public List<ActorMovie>? movies { get; set; }
    public string? name { get; set; }
    public string? photo { get; set; }
    public List<Profession>? profession { get; set; }
    public string? sex { get; set; }
}

public class Fact
{
    public string? value { get; set; }
}

public class ActorMovie
{
    public int? id { get; set; }
    public string? name { get; set; }
    public string? alternativeName { get; set; }
    public double? rating { get; set; }
    public bool? general { get; set; }
    public string? description { get; set; }
    public string? enProfession { get; set; }
}

public class Profession
{
    public string? value { get; set; }
}

public class ActorItem
{
    public List<ActorItemDoc>? docs { get; set; }
    public int? total { get; set; }
    public int? limit { get; set; }
    public int? page { get; set; }
    public int? pages { get; set; }
}
