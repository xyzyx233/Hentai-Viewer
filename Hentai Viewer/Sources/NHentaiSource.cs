using System;
using System.Composition;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Meowtrix.HentaiViewer.Sources
{
    [Export(typeof(Composition.IGallery))]
    class NHentaiSource : Composition.IGallery
    {
        public string Name => "NHentai";
        public UIElement SettingPage => new NHentaiSetting();
        public void LoadSettings(ApplicationDataContainer data)
        {
            //throw new NotImplementedException();
        }
        public void SaveSettings(ApplicationDataContainer data)
        {
            //throw new NotImplementedException();
        }
    }
}
