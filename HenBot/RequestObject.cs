using Newtonsoft.Json;

namespace HenBot
{
    public record RequestObject
    {
        [JsonProperty("@attributes")]
        public Attributes Attributes { get; set; }
        public List<Post> Post { get; set; } 
    }
}
