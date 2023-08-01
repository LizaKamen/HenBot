namespace HenBot
{
    public class TagExistenceChecker
    {
        public static string wrongTag;
        public static async Task<bool> CheckIfTagsExist(List<string> tags)
        {
            foreach (var tag in tags)
            {
                if (!await CheckIfTagQueryExists(tag))
                    return false;
            }
            return true;
        }

        public static async Task<bool> CheckIfTagQueryExists(string tagQuery)
        {
            var tags = tagQuery.Split('+');
            foreach (var tag in tags)
            {
                var request = $"https://gelbooru.com/index.php?page=dapi&s=tag&q=index&name={tag}&json=1";
                var response = await HttpRequester.MakeRequest(request);
                var tagObj = JsonDeserializer.DeserializeTag(response);
                if (tagObj.Tag == null)
                {
                    wrongTag = tag;
                    return false;
                }
            }
            return true;
            
        }
    }
}
