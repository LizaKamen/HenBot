using Newtonsoft.Json;

namespace HenBot
{
    public record TagObject
    {
        [JsonProperty("@attributes")]
        public Attributes Attributes { get; set; }

        public List<Tag> Tag { get; set; }
    }
}
