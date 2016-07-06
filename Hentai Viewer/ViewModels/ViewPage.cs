using Windows.UI.Xaml;

namespace Meowtrix.HentaiViewer.ViewModels
{
    interface ViewPage
    {
        UIElement Icon { get; }
        UIElement View { get; }
    }
}
