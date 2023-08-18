namespace HenBot;

public static class TagsQueryBuilder
{
    private static readonly string _alwaysBannedTags = " -animated";

    public static string BuildTagsQuery(string tags)
    {
        return tags + _alwaysBannedTags;
    }
}