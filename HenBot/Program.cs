using HenBot.Consts;
using HenBot.Handlers;
using HenBot.Repository;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace HenBot;

public class Program
{
    private static async Task Main(string[] args)
    {
        using var dbContext = new RepositoryContext();
        dbContext.Database.Migrate();

        var token = Appsettings.BotToken;
        if (token is null)
        {
            Console.WriteLine("Token is null");
            throw new Exception("Token is null");
        }
        var botClient = new TelegramBotClient(token);

        using CancellationTokenSource cts = new();

        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
        };

        botClient.StartReceiving(UpdateHandler.HandleUpdateAsync,
            PoolingErrorHandler.HandlePollingErrorAsync,
            receiverOptions,
            cts.Token);

        var me = await botClient.GetMe();

        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();

        // Send cancellation request to stop bot
        cts.Cancel();
    }
}
