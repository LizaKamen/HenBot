using RestSharp;

namespace HenBot;

public static class RestHttpClient
{
    private static RestClient? RestClient;

    public static RestClient GetRestClient()
    {
        if (RestClient != null)
            return RestClient;
        var options = new RestClientOptions("https://gelbooru.com/");
        RestClient = new RestClient(options);
        RestClient.AddDefaultHeader("User-Agent", Consts.userAgent);
        RestClient.AddDefaultParameter("api_key", Appsettings.ApiKey);
        RestClient.AddDefaultParameter("user_id", Appsettings.UserId);
        return RestClient;
    }
}