namespace HenBot
{
    public class UrlExtractor
    {
        public static List<string> ExtractUrlsFromPostsList(List<Post> postsList)
        {
            var urls = new List<string>(postsList.Capacity);
            foreach (var post in postsList)
            {
                if (post.Sample == 0)
                {
                    urls.Add(post.File_Url);
                }
                else
                {
                    urls.Add(post.Sample_Url);
                }
            }

            return urls;
        }
    }
}
