using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace Meowtrix.HentaiViewer
{
    internal static class HttpHost
    {
        private static readonly HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter
        {
            CookieUsageBehavior = HttpCookieUsageBehavior.Default
        };
        public static HttpClient Client { get; } = new HttpClient(filter);
        public static HttpCookieManager CookieManager => filter.CookieManager;
        static HttpHost()
        {
            Client.DefaultRequestHeaders.UserAgent.TryParseAdd(Settings.UAString);
            Client.DefaultRequestHeaders.Accept.TryParseAdd("text/html, application/xhtml+xml, image/jxr, */*");//不加会sad panda
            Client.DefaultRequestHeaders.AcceptLanguage.TryParseAdd("zh-Hans-CN,zh-Hans;q=0.8,en-US;q=0.6,en;q=0.4,ja;q=0.2");
        }
    }
}
