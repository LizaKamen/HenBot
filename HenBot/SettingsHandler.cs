using Telegram.Bot;
using Telegram.Bot.Types;

namespace HenBot;

public static class SettingsHandler
{
    static SavedUser userToSave = new SavedUser();
    public static async Task HandleSettings(ITelegramBotClient botClient, long chatId,
        CancellationToken cancellationToken)
    {
        var user = UserRepository.GetUser(chatId);
        user.IsConfiguring = true;
        UserRepository.UpdateUser(user);
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
                await ProcessStep0(botClient, update, chatId, cancellationToken);
                break;
            case 1:
                await ProcessStep1(botClient, update, chatId, cancellationToken);
                break;
        }
    }

    private static async Task ProcessStep0(ITelegramBotClient botClient, Update update, long chatId,
        CancellationToken cancellationToken)
    {
        if (int.TryParse(update.Message.Text, out var limit) && limit <= 10)
        {
            userToSave.Limit = limit;
            var user = UserRepository.GetUser(chatId);
            user.Step++;
            UserRepository.UpdateUser(user);

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
        CancellationToken cancellationToken)
    {
        var tagQueriesToCheck = update.Message.Text.Split(',').ToList();
        if (!await TagExistenceChecker.CheckIfTagsExist(tagQueriesToCheck))
        {   
            var user = UserRepository.GetUser(chatId);
            user.Step = 1;
            UserRepository.UpdateUser(user);
            await botClient.SendTextMessageAsync(
                chatId,
                $"There was a problem with {TagExistenceChecker.WrongTag} tag. Try again with correct spelling",
                cancellationToken: cancellationToken
            );
        }

        else
        {
            var tagQueries = new List<TagQuery>();
            foreach (var query in tagQueriesToCheck)
            {
                tagQueries.Add(new TagQuery() { Id = new Guid(), Query = query});
            }
            userToSave.SavedTags = tagQueries;
            await botClient.SendTextMessageAsync(
                chatId,
                $"Configuring ended, here are your settings: {userToSave.Limit} pics per post, saved tags: later))",
                cancellationToken: cancellationToken
            );
            userToSave.IsConfiguring = false;
            userToSave.Step = 0;
            userToSave.Id = chatId;
            UserRepository.UpdateUser(userToSave);
        }
    }
}