﻿using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace HenBot;

public static class AyayaHandler
{
    public static async Task HandleAyaya(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
    {
        var chatLocal = LocalChatRepository.GetLocalChat(chatId);
        chatLocal.IsAyaya = true;
        var savedChat = ChatRepository.GetChatFromDb(chatId);
        if (savedChat.SavedTags.Count == 0)
        {
            chatLocal.LastTag = "rating:general";
            await DoAyaya(botClient, "rating:general", chatId, chatLocal, cancellationToken);
        }

        else
            await botClient.SendMessage(
                chatId,
                "Choose tag",
                replyMarkup: CreateInlineKeyboard(savedChat),
                cancellationToken: cancellationToken);
    }

    public static async Task DoAyaya(ITelegramBotClient botClient, string tags, long chatId, LocalChat savedChat,
        CancellationToken cancellationToken)
    {
        var chat = ChatRepository.GetChatFromDb(chatId);
        var postsList =
            await GelbooruSourceService.GetPostsAsync(chat.Limit, tags, savedChat.Page);
        if (postsList == null)
        {
            await botClient.SendMessage(chatId, "There're no posts by your query, try again with different request", cancellationToken: cancellationToken);
            return;
        }
        var urls = UrlExtractor.ExtractUrlsFromPostsList(postsList);
        Console.WriteLine($"Chat: {chatId}, urls about to send to the chat: {JsonSerializer.Serialize(urls)}");
        var media = AlbumInputMediaCreator.CreateAlbumInputMedia(urls);
        await botClient.SendMediaGroup(chatId, media, cancellationToken: cancellationToken);
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