using Telegram.Bot;

namespace HenBot
{
    public static class StartHandler
    {
        public static async Task HandleStart(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: "Write /settings to initial configuration or /getAyaya to get anime pics",
                                            cancellationToken: cancellationToken);
        }
    }
}
