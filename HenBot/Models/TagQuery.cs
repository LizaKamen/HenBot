using System.ComponentModel.DataAnnotations.Schema;

namespace HenBot.Models;

public record TagQuery 
{
    public Guid Id { get; set; }
    public string Query { get; set; }

    [ForeignKey(nameof(Chat))]
    public long SavedChatId { get; set; }
    public Chat Chat { get; set; }
}