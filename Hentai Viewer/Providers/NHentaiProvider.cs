using System;
using System.Composition;
using System.Threading.Tasks;
using Meowtrix.HentaiViewer.ViewModels;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Meowtrix.HentaiViewer.Providers
{
    [Export(typeof(Composition.IGallery))]
    class NHentaiProvider : Composition.IGallery
    {
        private const string hostname = "//nhentai.net/";
        public string Name => "NHentai";
        public UIElement SettingPage => new NHentaiSettingPage { Settings = SettingInstance };
        public NHentaiSettings SettingInstance { get; } = new NHentaiSettings();
        public void LoadSettings(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata) => SettingInstance.Load(localdata, roamingdata);
        public void SaveSettings(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata) => SettingInstance.Save(localdata, roamingdata);
        private Uri hosturi => new Uri(SettingInstance.Scheme + hostname);
        public Task<SearchResult> SearchAsync(SearchInfo info, int page)
        {
            throw new NotImplementedException();
        }
    }
}
