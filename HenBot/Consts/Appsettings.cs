using Microsoft.Extensions.Configuration;

namespace HenBot.Consts;

public class Appsettings
{
    public static string BotToken { get { return _config["tokens:botToken"]; } }
    public static string ApiKey { get { return _config["tokens:api_key"]; } }
    public static string UserId { get { return _config["tokens:user_id"]; } }

    private static readonly IConfiguration _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
}