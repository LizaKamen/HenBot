using Telegram.Bot;

namespace HenBot.Handlers;

public static class StartHandler
{
    public static async Task HandleStart(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
    {
        await botClient.SendMessage(
            chatId,
            "Write /settings to initial configuration or /getAyaya to get anime pics",
            cancellationToken: cancellationToken);
    }
}