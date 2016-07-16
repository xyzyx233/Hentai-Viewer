using System;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Meowtrix.HentaiViewer.ViewModels;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Meowtrix.HentaiViewer.Providers
{
    [Export(typeof(Composition.IGallery))]
    class NHentaiProvider : Composition.IGallery
    {
        private const string hostname = "https://nhentai.net/";
        public string Name => "NHentai";
        public UIElement SettingPage => new NHentaiSettingPage();
        public void LoadSettings(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata)
        {
            //throw new NotImplementedException();
        }
        public void SaveSettings(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata)
        {
            //throw new NotImplementedException();
        }
        private static Uri hosturi = new Uri(hostname);
        public async Task<SearchResult> SearchAsync(SearchInfo info, int page)
        {
            HtmlDocument document = new HtmlDocument();
            var sb = new StringBuilder(hostname);
            sb.Append($"?page={page}");
            //TODO:add query
            try
            {
                document.LoadHtml(await HttpHost.Client.GetStringAsync(new Uri(sb.ToString())));
                return new SearchResult
                {
                    Provider = this,
                    SearchInfo = info,
                    PagesCount = int.Parse(document.DocumentNode.SelectSingleNode("//a[@class='last']").Attributes["href"].Value.Replace("?page=", "")),
                    Entries = document.DocumentNode.SelectNodes("//div[@class='gallery']/a")
                        .Select(node => new GalleryEntryInfo
                        {
                            Title = node.SelectSingleNode("div").InnerText,
                            Uri = new Uri(hosturi, node.Attributes["href"].Value),
                            ThumbnailUri = new Uri("https:" + node.SelectSingleNode("img").Attributes["src"].Value)
                        }).ToArray()
                };
            }
            catch
            {
                return new SearchResult
                {
                    Provider = this,
                    SearchInfo = info,
                    Entries = new GalleryEntryInfo[0]
                };
            }
        }
    }
}
