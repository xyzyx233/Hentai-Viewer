using System;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Meowtrix.HentaiViewer.ViewModels;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.Web.Http;

namespace Meowtrix.HentaiViewer.Providers
{
    [Export(typeof(Composition.IGallery))]
    class EHentaiProvider : Composition.IGallery
    {
        public EHentaiProvider()
        {
            HttpHost.CookieManager.SetCookie(new HttpCookie(nameof(uconfig), "e-hentai.org", "") { Value = uconfig });
            HttpHost.CookieManager.SetCookie(new HttpCookie(nameof(uconfig), "exhentai.org", "") { Value = uconfig });
        }
        private const string uconfig = "uh_y-lt_m-tl_r-tr_2-ts_m-prn_y-dm_t-ar_0-xns_0-rc_0-rx_0-ry_0-cs_a-fs_p-to_a-pn_0-sc_0-ru_rrggb-xr_a-cats_0-ms_n-mt_n-sa_y-oi_n-qb_n-tf_n-hp_-hk_-xl_";
        public string Name => "EHentai";
        public UIElement SettingPage => new EHentaiSettingPage { Settings = SettingInstance };
        public static EHentaiSettings SettingInstance { get; } = new EHentaiSettings();
        public void LoadSettings(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata) => SettingInstance.Load(localdata, roamingdata);
        public void SaveSettings(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata) => SettingInstance.Save(localdata, roamingdata);
        private string CurrentHostName
            => SettingInstance.IsLogin && SettingInstance.PreferExhentai ?
            "http://exhentai.org/" :
            "http://g.e-hentai.org/";
        public async Task<SearchResult> SearchAsync(SearchInfo info, int page)
        {
            HtmlDocument document = new HtmlDocument();
            var sb = new StringBuilder(CurrentHostName);
            sb.Append($"?page={page - 1}");
            //TODO:add query
            try
            {
                document.LoadHtml(await HttpHost.Client.GetStringAsync(new Uri(sb.ToString())));
                return new SearchResult
                {
                    Provider = this,
                    SearchInfo = info,
                    PagesCount = int.Parse(document.DocumentNode.SelectSingleNode("//table[@class='ptt']/tr/td[last()-1]/a").InnerText),
                    Entries = document.DocumentNode.SelectNodes("//div[@class='itg']/div[@class='id1']")
                        .Select(node => new GalleryEntryInfo
                        {
                            Title = node.SelectSingleNode("div[1]/a").InnerText,
                            Uri = new Uri(node.SelectSingleNode("div[1]/a").Attributes["href"].Value),
                            ThumbnailUri = new Uri(node.SelectSingleNode("div[2]/a/img").Attributes["src"].Value)
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
