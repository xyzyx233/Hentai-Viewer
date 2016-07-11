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
        public string Name => "NHentai";
        public UIElement SettingPage => new NHentaiSetting();
        public void LoadSettings(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata)
        {
            //throw new NotImplementedException();
        }
        public void SaveSettings(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata)
        {
            //throw new NotImplementedException();
        }
        public Task<ListPage> GetListAsync(SearchInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
