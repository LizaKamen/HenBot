namespace HenBot;

public record SavedUser
{
    public int Step { get; set; }
    public int Page { get; set; }
    public int Limit { get; set; } = 10;
    public bool IsConfiguring { get; set; }
    public bool IsAyaya { get; set; }
    public bool IsAyayaed { get; set; }
    public string LastTag { get; set; }
    public List<string> SavedTags { get; set; } = new();
}