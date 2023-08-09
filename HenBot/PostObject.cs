using Newtonsoft.Json;

namespace HenBot;

public record PostObject
{
    [JsonProperty("@attributes")] public Attributes Attributes { get; set; }

    [JsonProperty("post")] public List<Post> Post { get; set; }
}