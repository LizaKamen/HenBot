namespace HenBot.Models;

public record Chat
{
    public long Id { get; set; }
    public int Limit { get; set; } = 10;
    public List<TagQuery> SavedTags { get; set; } = new();
}