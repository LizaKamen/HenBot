namespace HenBot
{
    public class HttpRequester
    {
        public static async Task<string> MakeRequest(string request)
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "HenBot");

            return await client.GetStringAsync(request);
        }
    }
}
