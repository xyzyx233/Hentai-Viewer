using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// “内容对话框”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上进行了说明

namespace Meowtrix.HentaiViewer
{
    public sealed partial class SelectPageDialog : ContentDialog
    {
        public SelectPageDialog()
        {
            this.InitializeComponent();
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Input { get; set; }

        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter) this.Hide();
        }
    }
}
