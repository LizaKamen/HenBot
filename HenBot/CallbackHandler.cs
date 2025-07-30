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
        var savedChat = LocalChatRepository.GetLocalChat(chatId);
        if (savedChat.IsConfiguring)
            await SettingsHandler.CompleteConfiguration(botClient, update, chatId, cancellationToken);
        if (savedChat.IsAyaya)
        {
            savedChat.LastTag = update.CallbackQuery.Data;
            await AyayaHandler.DoAyaya(botClient, savedChat.LastTag, chatId, savedChat, cancellationToken);
        }
    }
}