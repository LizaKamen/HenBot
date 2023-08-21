namespace HenBot;

public record SavedUser
{
    public long Id { get; set; }
    public int Step { get; set; }
    public int Page { get; set; }
    public int Limit { get; set; } = 10;
    public bool IsConfiguring { get; set; }
    public bool IsAyaya { get; set; }
    public bool IsAyayaed { get; set; }
    public string LastTag { get; set; } = "";
    public List<TagQuery> SavedTags { get; set; } = new();
}