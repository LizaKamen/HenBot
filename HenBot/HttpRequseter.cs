using Telegram.Bot.Types;

namespace HenBot
{
    public class HttpRequseter
    {
        public static async Task<IAlbumInputMedia[]> SendRequest(int limit, string tags, Ratings rating, int page)
        {
            var alwaysBannedTags = "+-animated";
            var request = $"https://gelbooru.com/index.php?page=dapi&s=post&q=index&limit={limit}&tags={GenerateExcludeTagsString(rating) + tags + alwaysBannedTags}&pid={page}&json=1";
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "HenBot");
            var urls = await ProcessPostsAsync(client, request);
            
            
            return CreateAlbumInputMedia(urls);

            static async Task<List<string>> ProcessPostsAsync(HttpClient client, string request)
            {
                var json = await client.GetStringAsync(request);
                var postsList = JsonDeserializer.Deserialize(json);
                var urls = new List<string>(postsList.Post.Capacity);
                foreach (var post in postsList.Post)
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

            IAlbumInputMedia[] CreateAlbumInputMedia(List<string> urls)
            {
                List<IAlbumInputMedia> AlbumInput = new List<IAlbumInputMedia>(urls.Count);
                foreach (var url in urls)
                {
                    AlbumInput.Add(new InputMediaPhoto(InputFile.FromUri(url)));
                }
                var res = AlbumInput.ToArray();
                return res;
            }

            string GenerateExcludeTagsString(Ratings ratings)
            {
                switch(ratings)
                {
                    case Ratings.General:
                        return "rating%3ageneral+";
                    case Ratings.Sensitive:
                        return "-rating%3aquestionable+-rating%3aexplicit+";
                    case Ratings.Questionable:
                        return "-rating%3aexplicit+";
                    case Ratings.Explicit:
                        return "";
                    default:
                        goto case Ratings.General;
                }
            }
        }

    }
}
