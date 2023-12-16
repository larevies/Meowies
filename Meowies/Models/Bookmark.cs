namespace Meowies.Models;

public class Bookmark
{
    public ulong Id { get; set; }
    public int MovieId { get; set; }
    
    public User User { get; set; }
}