using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meowies.Models;

public class ActorDoc
{
    public int id { get; set; }
    public string name { get; set; }
    public string enName { get; set; }
    public string photo { get; set; }
    public string sex { get; set; }
    public int growth { get; set; }
    public object birthday { get; set; }
    public string death { get; set; }
    public int age { get; set; }
}

public class ActorList
{
    public List<ActorDoc> docs { get; set; }
    public int total { get; set; }
    public int limit { get; set; }
    public int page { get; set; }
    public int pages { get; set; }
}