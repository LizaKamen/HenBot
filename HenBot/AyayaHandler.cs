using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace HenBot
{
    public class AyayaHandler
    {
        public static async Task HandleAyaya(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
        {
            UserRepository.GetUser(chatId).IsAyaya = true;
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Choose tag",
                replyMarkup: CreateInlineKeyboard(UserRepository.GetUser(chatId)),
                cancellationToken: cancellationToken);
        }

        static InlineKeyboardMarkup CreateInlineKeyboard(SavedUser savedUser)
        {
            var length = savedUser.SavedTags.Count >= 10 ? 10 : savedUser.SavedTags.Count;
            var inlineKeyboard = new List<InlineKeyboardButton[]>(length);
            for (var i = 0; i < length; i++)
            {
                inlineKeyboard.Add(new[] { InlineKeyboardButton.WithCallbackData(text: savedUser.SavedTags[i], callbackData: savedUser.SavedTags[i]) });
            }
            return inlineKeyboard.ToArray();
        }

        public static async Task DoAyaya(ITelegramBotClient botClient, string tags, long chatId, SavedUser savedUser, CancellationToken cancellationToken)
        {
            await botClient.SendMediaGroupAsync(
                                                chatId: chatId,
                                                media: await HttpRequseter.SendRequest(savedUser.Limit, tags, savedUser.SettedRating, savedUser.Page),
                                                cancellationToken: cancellationToken);
            savedUser.IsAyaya = false;
            savedUser.IsAyayaed = true;
        }
    }
}
