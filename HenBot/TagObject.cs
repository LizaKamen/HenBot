using System.Text.Json.Serialization;

namespace HenBot;

public record TagObject
{
    [JsonPropertyName("@attributes")] public Attributes Attributes { get; set; }

    public List<Tag> Tag { get; set; }
}