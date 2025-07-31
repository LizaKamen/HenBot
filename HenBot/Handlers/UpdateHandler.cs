using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace HenBot.Handlers;

public static class UpdateHandler
{
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                MessageHandler.HandleMessage(update, botClient, cancellationToken);
                break;
            case UpdateType.CallbackQuery:
                CallbackHandler.HandleCallback(update, botClient, cancellationToken);
                break;
            default:
                return;
        }
    }
}