﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Meowtrix.HentaiViewer
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void NavigateToSetting(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingPage));
        }

        private ViewModels.ViewPageList ViewModel { get; } = new ViewModels.ViewPageList();

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (sender as ListView).ScrollIntoView(e.AddedItems[0], ScrollIntoViewAlignment.Leading);
        }
    }
}
