using HenBot.Helpers;
using HenBot.Models;
using RestSharp;

namespace HenBot.Services;

public static class GelbooruSourceService
{
    private static readonly RestClient _client = RestHttpClient.GetRestClient();

    public static async Task<List<Post>?> GetPostsAsync(int limit, string tags, int page)
    {
        var request = CreateRequest("post");
        request
            .AddParameter("limit", limit)
            .AddParameter("tags", TagsQueryBuilder.BuildTagsQuery(tags))
            .AddParameter("pid", page);
        var postObject = await _client.GetAsync<PostObject>(request);
        return postObject?.Post;
    }

    public static async Task<TagObject> GetTagAsync(string tag)
    {
        var request = CreateRequest("tag");
        request.AddParameter("name_pattern", tag);
        var tagObject = await _client.GetAsync<TagObject>(request);
        return tagObject;
    }

    private static RestRequest CreateRequest(string type)
    {
        var request = new RestRequest("index.php");
        request
            .AddParameter("page", "dapi")
            .AddParameter("s", type)
            .AddParameter("q", "index")
            .AddParameter("json", "1");
        return request;
    }
}