namespace HenBot;

public static class TagExistenceChecker
{
    public static string? WrongTag { get; set; }

    public static async Task<bool> CheckIfTagsExist(List<string> tags)
    {
        foreach (var tag in tags)
            if (!await CheckIfTagQueryExists(tag))
                return false;

        return true;
    }

    private static async Task<bool> CheckIfTagQueryExists(string tagQuery)
    {
        var tags = tagQuery.Split(' ');
        foreach (var tag in tags)
        {
            if(tag.Contains("user:"))
                continue;
            var tempTag = tag.Replace("~", "").Replace("{", "").Replace("}", "").Replace("*", "%").Replace(":general", "").Replace(":sensitive", "").Replace(":explicit", "").Replace("questionable", "");
            var tagRes = await GelbooruSourceService.GetTagAsync(tempTag);
            if (tagRes.Tag == null)
            {
                WrongTag = tag;
                return false;
            }
        }

        return true;
    }
}