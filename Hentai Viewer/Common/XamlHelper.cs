using Windows.UI.Xaml;

namespace Meowtrix.HentaiViewer
{
    internal static class XamlHelper
    {
        public static bool GetIsVisible(this UIElement obj) => obj.Visibility == Visibility.Visible;
        public static void SetIsVisible(this UIElement obj, bool value) => obj.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        public static bool GetIsNotVisible(this UIElement obj) => obj.Visibility != Visibility.Visible;
        public static void SetIsNotVisible(this UIElement obj, bool value) => obj.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
    }
}
