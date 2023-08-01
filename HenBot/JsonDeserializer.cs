using Newtonsoft.Json;

namespace HenBot
{
    public class JsonDeserializer
    {
        public static PostObject DeserializePost(string json)
        {
            var postObject = JsonConvert.DeserializeObject<PostObject>(json);
            return postObject;
        }

        public static TagObject DeserializeTag(string json)
        {
            var tagObject = JsonConvert.DeserializeObject<TagObject>(json);
            return tagObject;
        }
    }
}
