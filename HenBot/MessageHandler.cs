using Telegram.Bot;
using Telegram.Bot.Types;

namespace HenBot
{
    public class MessageHandler
    {
        public static async void HandleMessage(Update update, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {

            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            SavedUser savedUser = UserRepository.GetUser(chatId);

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            if (messageText != "/next")
            {
                savedUser.IsAyayaed = false;
                savedUser.Page = 0;
            }
            if (savedUser.IsConfiguring)
            {
                await SettingsHandler.CompleteConfiguration(botClient, update, chatId, cancellationToken);
            }
            switch (messageText)
            {
                case "/start":
                    await StartHandler.HandleStart(botClient, chatId ,cancellationToken);
                    break;
                case "/settings":
                    await SettingsHandler.HandleSettings(botClient, chatId, cancellationToken);
                    break;
                case "/getAyaya":
                    await AyayaHandler.HandleAyaya(botClient, chatId, cancellationToken);
                    break;
                case "/next":
                    savedUser.Page++;
                    await AyayaHandler.DoAyaya(botClient, savedUser.LastTag, chatId, savedUser, cancellationToken);
                    break;
                default:
                    return;
            }  
        }
    }
}
