using System.ComponentModel.DataAnnotations.Schema;

namespace HenBot;

public record TagQuery 
{
    public Guid Id { get; set; }
    public string Query { get; set; }
    [ForeignKey(nameof(Chat))]
    public long SavedUserId { get; set; }
    public Chat Chat { get; set; }
}