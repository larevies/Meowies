using Meowies.Models;
using Microsoft.EntityFrameworkCore;

namespace Meowies;

public sealed class MeowiesContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Bookmark> Bookmarks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=meowies.db");
    }
}