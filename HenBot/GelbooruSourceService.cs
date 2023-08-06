using RestSharp;

namespace HenBot
{
    public class GelbooruSourceService
    {
        static readonly RestClient _client = RestHttpClient.GetRestClient();

        public static async Task<List<Post>> GetPostsAsync(int limit, string tags, Ratings rating, int page)
        {
            var request = new RestRequest($"index.php?page=dapi&s=post&q=index&limit={limit}&tags={TagsQueryBuilder.BuildTagsQuery(rating, tags)}&pid={page}&json=1");
            var test = await _client.GetAsync(request);
            var postObject = await _client.GetAsync<PostObject>(request);
            return postObject.Post;
        }

        public static async Task<TagObject> GetTagAsync(string tag)
        {
            var request = new RestRequest($"index.php?page=dapi&s=tag&q=index&name={tag}&json=1");
            var tagObject = await _client.GetAsync<TagObject>(request);
            return tagObject;
        }
    }
}
