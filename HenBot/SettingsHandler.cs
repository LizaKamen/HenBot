using Telegram.Bot;
using Telegram.Bot.Types;

namespace HenBot;

public static class SettingsHandler
{
    static Chat chatToSave = new();
    public static async Task HandleSettings(ITelegramBotClient botClient, long chatId,
        CancellationToken cancellationToken)
    {
        var chat = LocalChatRepository.GetLocalChat(chatId);
        chat.IsConfiguring = true;
        await botClient.SendMessage(
            chatId,
            "Write amount of pics that u want to get per post",
            cancellationToken: cancellationToken);
    }

    public static async Task CompleteConfiguration(ITelegramBotClient botClient, Update update, long chatId,
        CancellationToken cancellationToken)
    {
        var savedChat = LocalChatRepository.GetLocalChat(chatId);
        switch (savedChat.Step)
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
            chatToSave.Limit = limit;
            var chat = LocalChatRepository.GetLocalChat(chatId);
            chat.Step++;

            await botClient.SendMessage(
                chatId,
                "Ok now write search queries. Like \"genshin_impact rating:general, blue_archive swimsuit rating:sensitive\" you can read more about searching <a href=\"https://gelbooru.com/index.php?page=wiki&s=&s=view&id=26263\">here</a>",
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                cancellationToken: cancellationToken
            );
        }

        else
        {
            await botClient.SendMessage(chatId, "Please enter a correct number",
                cancellationToken: cancellationToken);
        }
    }

    private static async Task ProcessStep1(ITelegramBotClient botClient, Update update, long chatId,
        CancellationToken cancellationToken)
    {
        var tagQueriesToCheck = update.Message.Text.Split(',').ToList();
        var chat = LocalChatRepository.GetLocalChat(chatId);
        if (!await TagExistenceChecker.CheckIfTagsExist(tagQueriesToCheck))
        {   
            chat.Step = 1;
            await botClient.SendMessage(
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
            chatToSave.SavedTags = tagQueries;
            await botClient.SendMessage(
                chatId,
                $"Configuring ended, here are your settings: {chatToSave.Limit} pics per post, saved tags: later))",
                cancellationToken: cancellationToken
            );
            chat.IsConfiguring = false;
            chat.Step = 0;
            chatToSave.Id = chatId;
            ChatRepository.OverrideChatLimitAndSavedTags(chatToSave);
        }
    }
}