using RestSharp;

namespace HenBot
{
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
            return RestClient;
        }
    }
}
