using HenBot.Models;
using Microsoft.EntityFrameworkCore;

namespace HenBot.Repository;

public static class ChatRepository
{
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
        db.SaveChanges();
    }

    public static void AddTag(long chatId, string tags)
    {
        using var db = new RepositoryContext();
        var chatFromDb = GetChatFromDb(chatId);
        db.SavedChats.Update(chatFromDb);
        chatFromDb.SavedTags.Add(new TagQuery() { Id = new Guid(), SavedChatId = chatId, Query = tags });
        db.SaveChanges();
    }
}