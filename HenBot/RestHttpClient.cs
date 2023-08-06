using RestSharp;

namespace HenBot
{
    public class RestHttpClient
    {
        private static RestClient? RestClient;

        public static RestClient GetRestClient()
        {
            if (RestClient == null)
            {
                var options = new RestClientOptions("https://gelbooru.com/");
                RestClient = new RestClient(options);
                RestClient.AddDefaultHeader("User-Agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
            }
            return RestClient;
        }
    }
}
