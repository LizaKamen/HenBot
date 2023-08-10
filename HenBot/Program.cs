﻿using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace HenBot;

public class Program
{
    private static async Task Main(string[] args)
    {
        var token = "YOUR TOKEN HERE";
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

        var me = await botClient.GetMeAsync();

        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();

        // Send cancellation request to stop bot
        cts.Cancel();
    }
}
