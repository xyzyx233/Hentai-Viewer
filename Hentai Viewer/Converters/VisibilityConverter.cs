using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Meowtrix.HentaiViewer.Converters
{
    class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string para = parameter?.ToString() ?? "";
            bool v = (bool)value;
            if (para == "")
                return v ? Visibility.Visible : Visibility.Collapsed;
            else if (para == "Reverse")
                return v ? Visibility.Collapsed : Visibility.Visible;
            else throw new ArgumentException(nameof(parameter));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string para = parameter?.ToString() ?? "";
            Visibility v = (Visibility)value;
            if (para == "")
                return v == Visibility.Visible;
            else if (para == "Reverse")
                return v == Visibility.Collapsed;
            else throw new ArgumentException(nameof(parameter));
        }
    }
}
