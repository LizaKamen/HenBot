using Microsoft.EntityFrameworkCore;

namespace HenBot;

public static class ChatRepository
{

    private static readonly Dictionary<long, Chat> savedChats = new();
    public static Chat GetChatLocaly (long chatId)
    {
        if (!savedChats.ContainsKey(chatId)) savedChats[chatId] = new Chat { Step = 0 };

        return savedChats[chatId];
    }

    public static Chat GetChatFromDb(long chatId)
    {
        using var db = new RepositoryContext();
        var chat = db.SavedChats
            .Include(tg => tg.SavedTags)
            .FirstOrDefault(u => u.Id == chatId);

        if (chat is not null)
            return chat;

        chat = new Chat { Id = chatId };
        db.Add(chat);
        db.SaveChanges();
        return chat;
    }

    public static void OverrideChatLimitAndSavedTags (Chat chatToSave)
    {
        using var db = new RepositoryContext();
        var chatFromDb = GetChatFromDb(chatToSave.Id);
        db.SavedChats.Update(chatFromDb);
        chatFromDb.Limit = chatToSave.Limit;
        chatFromDb.SavedTags = chatToSave.SavedTags;
        savedChats[chatToSave.Id] = chatToSave;
        db.SaveChanges();
    }
}