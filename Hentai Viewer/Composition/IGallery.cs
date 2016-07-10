using Meowtrix.HentaiViewer.ViewModels;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Meowtrix.HentaiViewer.Composition
{
    public interface IGallery
    {
        string Name { get; }
        UIElement SettingPage { get; }
        void LoadSettings(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata);
        void SaveSettings(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata);
        ListPage GetList(SearchInfo info = null);
    }
}
