using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HenBot
{

    public class SettingsHandler
    {
        public static async Task HandleSettings(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
        {
            UserRepository.GetUser(chatId).IsConfiguring = true;
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Write amount of pics that u want to get per post",
                cancellationToken: cancellationToken);
        }

        public static async Task CompleteConfiguration(ITelegramBotClient botClient, Update update, long chatId, CancellationToken cancellationToken)
        {
            var savedUser = UserRepository.GetUser(chatId);
            switch (savedUser.Step)
            {
                case 0:
                    if (int.TryParse(update.Message.Text, out int limit) && limit <= 10)
                    {
                        savedUser.Limit = limit;
                        savedUser.Step++;
                        await botClient.SendTextMessageAsync(
                            chatId,
                            "Ok now choose a rating.",
                        replyMarkup: inlineKeyboard,
                            cancellationToken: cancellationToken);
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(chatId, "Please enter correct number", cancellationToken: cancellationToken);
                    }
                    break;
                case 1:
                    switch (update.CallbackQuery.Data)
                    {
                        case "1":
                            savedUser.SettedRating = Ratings.General;
                            goto case "5";
                        case "2":
                            savedUser.SettedRating = Ratings.Sensitive;
                            goto case "5";
                        case "3":
                            savedUser.SettedRating = Ratings.Questionable;
                            goto case "5";
                        case "4":
                            savedUser.SettedRating = Ratings.Explicit;
                            goto case "5";
                        case "5":
                            savedUser.Step++;
                            await botClient.SendTextMessageAsync(
                                chatId,
                            "Ok now write few tags",
                                cancellationToken: cancellationToken);
                            break;
                        default:
                            goto case "1";
                    }
                    break;
                case 2:
                    savedUser.SavedTags = update.Message.Text.Split(' ').ToList();
                    savedUser.Step++;
                    await botClient.SendTextMessageAsync(chatId, $"Configuring ended here's your settings: {savedUser.Limit} pics per post, rating : {savedUser.SettedRating}, saved tags: later))", cancellationToken: cancellationToken);
                    savedUser.IsConfiguring = false;
                    savedUser.Step = 0;
                    break;
                default:
                    break;
            }
        }

        static InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                // first row
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "General", callbackData: "1"),
                    InlineKeyboardButton.WithCallbackData(text: "Sensitive", callbackData: "2"),
                },
                // second row
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Questionable", callbackData: "3"),
                    InlineKeyboardButton.WithCallbackData(text: "Explicit", callbackData: "4"),
                },
            });
    }
}
