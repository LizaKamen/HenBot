using HenBot.Repository;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HenBot.Handlers;

public static class MessageHandler
{
    public static async void HandleMessage(Update update, ITelegramBotClient botClient,
        CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
            return;
        if (message.Text is not { } messageText)
            return;
        var chatId = message.Chat.Id;
        var savedChat = LocalChatRepository.GetLocalChat(chatId);
        Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
        if (messageText != "/next")
        {
            savedChat.IsAyayaed = false;
            savedChat.Page = 0;
        }

        if (savedChat.IsConfiguring)
        {
            await SettingsHandler.CompleteConfiguration(botClient, update, chatId, cancellationToken);
            return;
        }

        switch (messageText)
        {
            case "/start":
                await StartHandler.HandleStart(botClient, chatId, cancellationToken);
                break;
            case "/settings":
                await SettingsHandler.HandleSettings(botClient, chatId, cancellationToken);
                break;
            case "/getAyaya":
                await AyayaHandler.HandleAyaya(botClient, chatId, cancellationToken);
                break;
            case "/next":
                if (savedChat.IsAyayaed)
                {
                    savedChat.Page++;
                    await AyayaHandler.DoAyaya(botClient, savedChat.LastTag, chatId, savedChat, cancellationToken);
                }

                break;
            default:
                return;
        }
    }
}