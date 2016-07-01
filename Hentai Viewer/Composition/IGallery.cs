using Windows.Storage;
using Windows.UI.Xaml;

namespace Meowtrix.HentaiViewer.Composition
{
    public interface IGallery
    {
        string Name { get; }
        UIElement SettingPage { get; }
        void LoadSettings(ApplicationDataContainer data);
        void SaveSettings(ApplicationDataContainer data);
    }
}
