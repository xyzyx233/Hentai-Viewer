using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Meowtrix.HentaiViewer.Providers
{
    public sealed partial class EHentaiSettingPage : UserControl
    {
        public EHentaiSettingPage()
        {
            this.InitializeComponent();
        }
        internal EHentaiSettings Settings { get; set; }
        private void Password_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                Settings.Login();
                e.Handled = true;
            }
        }
    }
}
