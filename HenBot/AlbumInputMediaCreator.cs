using Telegram.Bot.Types;

namespace HenBot
{
    public class AlbumInputMediaCreator
    {
        public static async Task<IAlbumInputMedia[]> CreateAlbumInputMedia(int limit, string tags, Ratings rating, int page)
        {
            var postsList = await GelbooruSourceService.GetPostsAsync(limit, tags, rating, page);

            var urls = UrlExtractor.ExtractUrlsFromPostsList(postsList);

            var AlbumInput = new List<IAlbumInputMedia>(urls.Count);
            foreach (var url in urls)
            {
                AlbumInput.Add(new InputMediaPhoto(InputFile.FromUri(url)));
            }
            var res = AlbumInput.ToArray();
            return res;
        }

    }
}
