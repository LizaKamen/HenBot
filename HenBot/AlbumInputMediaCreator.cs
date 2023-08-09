using Telegram.Bot.Types;

namespace HenBot;

public static class AlbumInputMediaCreator
{
    public static IAlbumInputMedia[] CreateAlbumInputMedia(List<string> urls)
    {
        var albumInput = new List<IAlbumInputMedia>(urls.Count);
        foreach (var url in urls)
            albumInput.Add(new InputMediaPhoto(InputFile.FromUri(url)));
        var res = albumInput.ToArray();
        return res;
    }
}