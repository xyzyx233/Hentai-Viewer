using Windows.Web.Http;

namespace Meowtrix.HentaiViewer
{
    internal static class HttpHost
    {
        public static HttpClient Client { get; } = new HttpClient();
        static HttpHost()
        {
            Client.DefaultRequestHeaders.UserAgent.TryParseAdd(Settings.UAString);
        }
    }
}
