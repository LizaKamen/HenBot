﻿using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace HenBot
{
    public static class AyayaHandler
    {
        public static async Task HandleAyaya(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
        {
            var savedUser = UserRepository.GetUser(chatId);
            UserRepository.GetUser(chatId).IsAyaya = true;
            if (savedUser.SavedTags.Count == 0)
                await DoAyaya(botClient, "", chatId, savedUser, cancellationToken);
            else
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Choose tag",
                    replyMarkup: CreateInlineKeyboard(UserRepository.GetUser(chatId)),
                    cancellationToken: cancellationToken);
            }
        }

        public static async Task DoAyaya(ITelegramBotClient botClient, string tags, long chatId, SavedUser savedUser, CancellationToken cancellationToken)
        {
            var postsList = await GelbooruSourceService.GetPostsAsync(savedUser.Limit, tags, savedUser.SettedRating, savedUser.Page);
            var urls = UrlExtractor.ExtractUrlsFromPostsList(postsList);
            var media = AlbumInputMediaCreator.CreateAlbumInputMedia(urls);
            await botClient.SendMediaGroupAsync(chatId, media, cancellationToken: cancellationToken);
            savedUser.IsAyaya = false;
            savedUser.IsAyayaed = true;
        }

        private static InlineKeyboardMarkup CreateInlineKeyboard(SavedUser savedUser)
        {
            var length = savedUser.SavedTags.Count >= 10 ? 10 : savedUser.SavedTags.Count;
            var inlineKeyboard = new List<InlineKeyboardButton[]>(length);
            for (var i = 0; i < length; i++)
            {
                inlineKeyboard.Add(new[] { InlineKeyboardButton.WithCallbackData(text: savedUser.SavedTags[i], callbackData: savedUser.SavedTags[i]) });
            }

            return inlineKeyboard.ToArray();
        }
    }
}
