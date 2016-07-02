using Windows.UI.Xaml;

namespace Meowtrix.HentaiViewer
{
    internal static class XamlHelper
    {
        public static bool GetIsVisible(UIElement obj) => obj.Visibility == Visibility.Visible;
        public static void SetIsVisible(UIElement obj, bool value) => obj.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        public static bool GetIsNotVisible(UIElement obj) => obj.Visibility != Visibility.Visible;
        public static void SetIsNotVisible(UIElement obj, bool value) => obj.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
    }
}
