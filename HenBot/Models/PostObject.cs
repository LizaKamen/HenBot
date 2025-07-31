using System.Text.Json.Serialization;

namespace HenBot.Models;

public record PostObject
{
    [JsonPropertyName("@attributes")] public Attributes Attributes { get; set; }

    [JsonPropertyName("post")] public List<Post> Post { get; set; }
}