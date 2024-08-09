namespace Meowies.Models;

/*
public class Bookmark
{
    public ulong Id { get; set; }
    public int MovieId { get; set; }
    
    public User User { get; set; }
}

*/

public class Bookmark
{
    public ulong Id { get; set; }
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public override string ToString()
    {
        return $"Id: {Id}, Movie Id: {MovieId}, User Id: {UserId}";
    }
}