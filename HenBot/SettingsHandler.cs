using Telegram.Bot;
using Telegram.Bot.Types;

namespace HenBot;

public static class SettingsHandler
{
    static SavedUser userToSave = new SavedUser();
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
        }
    }

    private static async Task ProcessStep0(ITelegramBotClient botClient, Update update, long chatId,
        SavedUser savedUser,
        CancellationToken cancellationToken)
    {
        if (int.TryParse(update.Message.Text, out var limit) && limit <= 10)
        {
            userToSave.Limit = limit;
            savedUser.Step ++;

            await botClient.SendTextMessageAsync(
                chatId,
                "Ok now write search queries.",
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
        var tagQueriesToCheck = update.Message.Text.Split(',').ToList();
        if (!await TagExistenceChecker.CheckIfTagsExist(tagQueriesToCheck))
        {
            savedUser.Step = 2;
            await botClient.SendTextMessageAsync(
                chatId,
                $"There was a problem with {TagExistenceChecker.WrongTag} tag. Try again with correct spelling",
                cancellationToken: cancellationToken
            );
        }

        else
        {
            userToSave.SavedTags = tagQueriesToCheck;
            await botClient.SendTextMessageAsync(
                chatId,
                $"Configuring ended, here are your settings: {userToSave.Limit} pics per post, saved tags: later))",
                cancellationToken: cancellationToken
            );
            userToSave.IsConfiguring = false;
            userToSave.Step = 0;
            UserRepository.UpdateUser(chatId, userToSave);
        }
    }
}