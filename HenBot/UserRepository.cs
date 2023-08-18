namespace HenBot;

public static class UserRepository
{
    private static readonly Dictionary<long, SavedUser> savedUsers = new();

    public static SavedUser GetUser(long chatId)
    {
        if (!savedUsers.ContainsKey(chatId)) savedUsers[chatId] = new SavedUser { Step = 0 };

        return savedUsers[chatId];
    }

    public static void UpdateUser (long chatId, SavedUser userToSave)
    {
        savedUsers[chatId] = userToSave;
    }
}