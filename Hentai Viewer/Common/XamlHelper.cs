using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Meowtrix.HentaiViewer
{
    internal static class XamlHelper
    {
        public static bool GetIsVisible(this UIElement obj) => obj.Visibility == Visibility.Visible;
        public static void SetIsVisible(this UIElement obj, bool value) => obj.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        public static bool GetIsNotVisible(this UIElement obj) => obj.Visibility != Visibility.Visible;
        public static void SetIsNotVisible(this UIElement obj, bool value) => obj.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
        public static T FindFirstChild<T>(this FrameworkElement element) where T : FrameworkElement
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(element);
            var children = new FrameworkElement[childrenCount];

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
                children[i] = child;
                if (child is T)
                    return (T)child;
            }

            for (int i = 0; i < childrenCount; i++)
                if (children[i] != null)
                {
                    var subChild = FindFirstChild<T>(children[i]);
                    if (subChild != null)
                        return subChild;
                }

            return null;
        }
    }
}
