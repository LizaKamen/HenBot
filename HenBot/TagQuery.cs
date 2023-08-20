using System.ComponentModel.DataAnnotations.Schema;

namespace HenBot;

public record TagQuery 
{
    public Guid Id { get; set; }
    public string Query { get; set; }
    [ForeignKey(nameof(SavedUser))]
    public long SavedUserId { get; set; }
    public SavedUser SavedUser { get; set; }
}