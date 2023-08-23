namespace HenBot;

public static class LocalChatRepository 
{
    private static readonly Dictionary<long, Chat> savedChats = new();
    public static Chat GetChatLocaly (long chatId)
    {
        if (!savedChats.ContainsKey(chatId)) savedChats[chatId] = new Chat { Step = 0 };

        return savedChats[chatId];
    }    
}