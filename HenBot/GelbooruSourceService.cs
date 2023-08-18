using RestSharp;

namespace HenBot;

public static class GelbooruSourceService
{
    private static readonly RestClient _client = RestHttpClient.GetRestClient();

    public static async Task<List<Post>> GetPostsAsync(int limit, string tags, int page)
    {
        var request = new RestRequest("index.php");
        request
            .AddParameter("page", "dapi")
            .AddParameter("s", "post")
            .AddParameter("q", "index")
            .AddParameter("limit", limit)
            .AddParameter("tags", TagsQueryBuilder.BuildTagsQuery(tags))
            .AddParameter("pid", page)
            .AddParameter("json", "1");
        var postObject = await _client.GetAsync<PostObject>(request);
        return postObject.Post;
    }

    public static async Task<TagObject> GetTagAsync(string tag)
    {
        var request = new RestRequest("index.php");
        request
            .AddParameter("page", "dapi")
            .AddParameter("s", "tag")
            .AddParameter("q", "index")
            .AddParameter("name_pattern", tag)
            .AddParameter("json", "1");
        var tagObject = await _client.GetAsync<TagObject>(request);
        return tagObject;
    }
}