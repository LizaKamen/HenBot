namespace HenBot
{
    public static class UrlExtractor
    {
        public static List<string> ExtractUrlsFromPostsList(List<Post> postsList)
        {
            return postsList
                .Select(p => p.Sample == 0 ? p.File_Url : p.Sample_Url)
                .ToList();
        }
    }
}
