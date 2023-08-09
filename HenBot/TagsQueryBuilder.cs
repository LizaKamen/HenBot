namespace HenBot
{
    public static class TagsQueryBuilder
    {
        static readonly string _alwaysBannedTags = " -animated";
        public static string BuildTagsQuery(Ratings rating, string tags) => GenerateExcludeTagsString(rating) + tags + _alwaysBannedTags;

        static string GenerateExcludeTagsString(Ratings ratings)
        {
            switch (ratings)
            {
                case Ratings.Sensitive:
                    return "-rating:questionable -rating:explicit ";
                case Ratings.Questionable:
                    return "-rating:explicit ";
                case Ratings.Explicit:
                    return "";
                default:
                    return "rating:general ";
            }
        }
    }
}
