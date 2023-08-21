﻿using Microsoft.EntityFrameworkCore;

namespace HenBot;

public static class UserRepository
{

    private static readonly Dictionary<long, SavedUser> savedUsers = new();

    public static SavedUser GetUser(long chatId)
    {
        using var db = new RepositoryContext();
        var user = db.SavedUsers
            .Include(tg => tg.SavedTags)
            .FirstOrDefault(u => u.Id == chatId);

        if (user is not null)
            return user;

        user = new SavedUser { Id = chatId };
        db.Add(user);
        db.SaveChanges();
        return user;
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