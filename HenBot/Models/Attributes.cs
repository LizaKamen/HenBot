namespace HenBot.Models;

public record Attributes
{
    public int Limit { get; set; }
    public int Offset { get; set; }
    public int Count { get; set; }
}