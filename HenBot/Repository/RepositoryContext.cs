using HenBot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HenBot.Repository;

public class RepositoryContext : DbContext
{
    public DbSet<Chat>? SavedChats { get; set; }
    public DbSet<TagQuery>? TagQuery { get; set; } 

    public string DbPath { get; }

    public RepositoryContext()
    {
        var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

        DbPath = isDocker
            ? "/app/data/henbot.db"
            : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "henbot.db");

        /*var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "henbot.db");*/
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .EnableSensitiveDataLogging()
            .UseSqlite($"Data Source={DbPath}");
        base.OnConfiguring(optionsBuilder);
    }
}