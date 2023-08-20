using Microsoft.EntityFrameworkCore;

namespace HenBot;

public static class UserRepository
{

    private static readonly Dictionary<long, SavedUser> savedUsers = new();

    public static SavedUser GetUser(long chatId)
    {
        using var db = new RepositoryContext();
        if (db.SavedUsers.Find(chatId) == null)
        {
            db.Add(new SavedUser {Id = chatId});
            db.SaveChanges();
        }
        return db.SavedUsers.Include(tg => tg.SavedTags).ToList().FirstOrDefault(u => u.Id == chatId);
    }

    public static void UpdateUser (SavedUser userToSave)
    {
        using var db = new RepositoryContext();
        var userFromDb = GetUser(userToSave.Id);
        db.SavedUsers.Remove(userFromDb);
        userFromDb = userToSave;
        db.SavedUsers.Update(userFromDb);
        db.SavedUsers.Add(userToSave);
        db.SaveChanges();
    }
}