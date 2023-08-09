using Telegram.Bot;
using Telegram.Bot.Types;

namespace HenBot;

public static class CallbackHandler
{
    public static async void HandleCallback(Update update, ITelegramBotClient botClient,
        CancellationToken cancellationToken)
    {
        var chatId = update.CallbackQuery.Message.Chat.Id;
        Console.WriteLine($"Received a '{update.Type}' message in chat {chatId}.");
        var savedUser = UserRepository.GetUser(chatId);
        if (savedUser.IsConfiguring)
            await SettingsHandler.CompleteConfiguration(botClient, update, chatId, cancellationToken);
        if (savedUser.IsAyaya)
        {
            savedUser.LastTag = update.CallbackQuery.Data;
            await AyayaHandler.DoAyaya(botClient, savedUser.LastTag, chatId, savedUser, cancellationToken);
        }
    }
}