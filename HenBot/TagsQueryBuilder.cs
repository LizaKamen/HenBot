namespace HenBot
{
    public class TagsQueryBuilder
    {
        static readonly string _alwaysBannedTags = "+-animated";
        public static string BuildTagsQuery(Ratings rating, string tags)
        {
            return GenerateExcludeTagsString(rating) + tags + _alwaysBannedTags;
        }

        static string GenerateExcludeTagsString(Ratings ratings)
        {
            switch (ratings)
            {
                case Ratings.Sensitive:
                    return "-rating%3aquestionable+-rating%3aexplicit+";
                case Ratings.Questionable:
                    return "-rating%3aexplicit+";
                case Ratings.Explicit:
                    return "";
                default:
                    return "rating%3ageneral+";
            }
        }
    }
}
