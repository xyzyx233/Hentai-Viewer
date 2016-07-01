using System.Composition;
using Windows.UI.Xaml;

namespace Meowtrix.HentaiViewer.Sources
{
    [Export(typeof(Composition.IGallery))]
    class NHentaiSource : Composition.IGallery
    {
        public string Name => "NHentai";
        public UIElement SettingPage => new NHentaiSetting();
    }
}
