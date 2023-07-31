namespace HenBot
{
    internal class UserRepository
    {
        private static Dictionary<long, SavedUser> savedUsers = new Dictionary<long, SavedUser>();

        public static SavedUser GetUser(long chatId)
        {
            if (!savedUsers.ContainsKey(chatId))
            {
                savedUsers[chatId] = new SavedUser { Step = 0 };
            }

            return savedUsers[chatId];
        }
    }
}
