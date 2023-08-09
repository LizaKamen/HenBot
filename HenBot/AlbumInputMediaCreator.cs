using Telegram.Bot.Types;

namespace HenBot
{
    public static class AlbumInputMediaCreator
    {
        public static IAlbumInputMedia[] CreateAlbumInputMedia(List<string> urls)
        {
            var AlbumInput = new List<IAlbumInputMedia>(urls.Count);
            foreach (var url in urls)
                AlbumInput.Add(new InputMediaPhoto(InputFile.FromUri(url)));
            var res = AlbumInput.ToArray();
            return res;
        }
    }
}
