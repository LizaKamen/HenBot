using Microsoft.EntityFrameworkCore;

namespace HenBot;

public class RepositoryContext : DbContext
{
    public DbSet<SavedUser>? SavedUsers { get; set; }
    public DbSet<TagQuery>? TagQuery { get; set; } 

    public string DbPath { get; }

    public RepositoryContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "users.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseSqlite($"Data Source={DbPath}");
}