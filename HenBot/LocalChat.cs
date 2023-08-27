namespace HenBot;

public record LocalChat
{
    public long Id { get; set; }
    public int Step { get; set; }
    public int Page { get; set; }
    public bool IsConfiguring { get; set; }
    public bool IsAyaya { get; set; }
    public bool IsAyayaed { get; set; }
    public string LastTag { get; set; } = "";
}