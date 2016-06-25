using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Meowtrix.HentaiViewer
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
        }
        private Settings CurrentSetting { get; } = Settings.Current;

        private void Password_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                if (sender == ehpasswd)
                    CurrentSetting.EhentaiSettings.Login();
                e.Handled = true;
            }
        }
    }
}
