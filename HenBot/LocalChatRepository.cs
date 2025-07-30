namespace HenBot;

public static class LocalChatRepository 
{
    private static readonly Dictionary<long, LocalChat> savedChats = new();
    public static LocalChat GetLocalChat (long chatId)
    {
        if (!savedChats.ContainsKey(chatId)) savedChats[chatId] = new LocalChat { Step = 0 };

        return savedChats[chatId];
    }    
}