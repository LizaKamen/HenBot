using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace HenBot;

public static class AyayaHandler
{
    public static async Task HandleAyaya(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
    {
        var chatLocal = LocalChatRepository.GetChatLocaly(chatId);
        chatLocal.IsAyaya = true;
        var savedChat = ChatRepository.GetChatFromDb(chatId);
        chatLocal.Limit = savedChat.Limit;
        if (savedChat.SavedTags.Count == 0)
        {
            chatLocal.LastTag = "rating:general";
            await DoAyaya(botClient, "rating:general", chatId, chatLocal, cancellationToken);
        }

        else
            await botClient.SendTextMessageAsync(
                chatId,
                "Choose tag",
                replyMarkup: CreateInlineKeyboard(savedChat),
                cancellationToken: cancellationToken);
    }

    public static async Task DoAyaya(ITelegramBotClient botClient, string tags, long chatId, Chat savedChat,
        CancellationToken cancellationToken)
    {
        var postsList =
            await GelbooruSourceService.GetPostsAsync(savedChat.Limit, tags, savedChat.Page);
        if (postsList == null)
        {
            await botClient.SendTextMessageAsync(chatId, "There're no posts by your query, try again with different request", cancellationToken: cancellationToken);
            return;
        }
        var urls = UrlExtractor.ExtractUrlsFromPostsList(postsList);
        Console.WriteLine($"Chat: {chatId}, urls about to send to the chat: {JsonConvert.SerializeObject(urls)}");
        var media = AlbumInputMediaCreator.CreateAlbumInputMedia(urls);
        await botClient.SendMediaGroupAsync(chatId, media, cancellationToken: cancellationToken);
        savedChat.IsAyaya = false;
        savedChat.IsAyayaed = true;
    }

    private static InlineKeyboardMarkup CreateInlineKeyboard(Chat savedChat)
    {
        var length = savedChat.SavedTags.Count >= 10 ? 10 : savedChat.SavedTags.Count;
        var inlineKeyboard = new List<InlineKeyboardButton[]>(length);
        for (var i = 0; i < length; i++)
            inlineKeyboard.Add(new[]
                { InlineKeyboardButton.WithCallbackData(savedChat.SavedTags[i].Query, savedChat.SavedTags[i].Query) });

        return inlineKeyboard.ToArray();
    }
}