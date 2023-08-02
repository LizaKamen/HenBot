using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HenBot;

public static class SettingsHandler
{
    private static readonly InlineKeyboardMarkup inlineKeyboard = new(new[]
    {
        // first row
        new[]
        {
            InlineKeyboardButton.WithCallbackData("General", "1"),
            InlineKeyboardButton.WithCallbackData("Sensitive", "2")
        },
        // second row
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Questionable", "3"),
            InlineKeyboardButton.WithCallbackData("Explicit", "4")
        }
    });

    public static async Task HandleSettings(ITelegramBotClient botClient, long chatId,
        CancellationToken cancellationToken)
    {
        UserRepository.GetUser(chatId).IsConfiguring = true;
        await botClient.SendTextMessageAsync(
            chatId,
            "Write amount of pics that u want to get per post",
            cancellationToken: cancellationToken);
    }

    public static async Task CompleteConfiguration(ITelegramBotClient botClient, Update update, long chatId,
        CancellationToken cancellationToken)
    {
        var savedUser = UserRepository.GetUser(chatId);

        switch (savedUser.Step)
        {
            case 0:
                await ProcessStep0(botClient, update, chatId, savedUser, cancellationToken);
                break;
            case 1:
                await ProcessStep1(botClient, update, chatId, savedUser, cancellationToken);
                break;
            case 2:
                await ProcessStep2(botClient, update, chatId, savedUser, cancellationToken);
                break;
        }
    }

    private static async Task ProcessStep0(ITelegramBotClient botClient, Update update, long chatId,
        SavedUser savedUser,
        CancellationToken cancellationToken)
    {
        if (int.TryParse(update.Message.Text, out var limit) && limit <= 10)
        {
            savedUser.Limit = limit;
            savedUser.Step++;

            await botClient.SendTextMessageAsync(
                chatId,
                "Ok now choose a rating.",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken
            );
        }
        else
        {
            await botClient.SendTextMessageAsync(chatId, "Please enter a correct number",
                cancellationToken: cancellationToken);
        }
    }

    private static async Task ProcessStep1(ITelegramBotClient botClient, Update update, long chatId,
        SavedUser savedUser,
        CancellationToken cancellationToken)
    {
        switch (update.CallbackQuery.Data)
        {
            case "1":
                savedUser.SettedRating = Ratings.General;
                break;
            case "2":
                savedUser.SettedRating = Ratings.Sensitive;
                break;
            case "3":
                savedUser.SettedRating = Ratings.Questionable;
                break;
            case "4":
                savedUser.SettedRating = Ratings.Explicit;
                break;
            default:
                savedUser.SettedRating = Ratings.General;
                break;
        }

        savedUser.Step++;
        await botClient.SendTextMessageAsync(
            chatId,
            "Ok now write a few tags",
            cancellationToken: cancellationToken
        );
    }

    private static async Task ProcessStep2(ITelegramBotClient botClient, Update update, long chatId,
        SavedUser savedUser,
        CancellationToken cancellationToken)
    {
        var tagsToCheck = update.Message.Text.Split(' ').ToList();
        if (!await TagExistenceChecker.CheckIfTagsExist(tagsToCheck))
        {
            savedUser.IsConfiguring = false;
            savedUser.Step = 0;

            await botClient.SendTextMessageAsync(
                chatId,
                $"There was a problem with {TagExistenceChecker.wrongTag} tag. Try again with correct spelling",
                cancellationToken: cancellationToken
            );
        }
        else
        {
            savedUser.SavedTags = tagsToCheck;
            await botClient.SendTextMessageAsync(
                chatId,
                $"Configuring ended, here are your settings: {savedUser.Limit} pics per post, rating: {savedUser.SettedRating}, saved tags: later))",
                cancellationToken: cancellationToken
            );

            savedUser.IsConfiguring = false;
            savedUser.Step = 0;
        }
    }
}