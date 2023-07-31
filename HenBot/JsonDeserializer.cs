using Newtonsoft.Json;

namespace HenBot
{
    public class JsonDeserializer
    {
        public static RequestObject Deserialize(string json)
        {
            var ros = JsonConvert.DeserializeObject<RequestObject>(json);
            return ros;
        }
    }
}
