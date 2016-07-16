using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Meowtrix.HentaiViewer.Controls
{
    public class GalleryListView : ListView
    {
        private ScrollViewer _scrollViewer;
        private ItemsStackPanel _panel;

        public int FirstVisibleIndex
        {
            get { return (int)GetValue(FirstVisibleIndexProperty); }
            set { SetValue(FirstVisibleIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FirstVisibleIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FirstVisibleIndexProperty =
            DependencyProperty.Register(nameof(FirstVisibleIndex), typeof(int), typeof(GalleryListView), new PropertyMetadata(0));

        public GalleryListView()
        {
            this.Loaded += ListViewEx_Loaded;
            this.Unloaded += ListViewEx_Unloaded;
        }

        private void ListViewEx_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= ListViewEx_Unloaded;
            if (_scrollViewer != null)
                _scrollViewer.ViewChanged -= Sv_ViewChanged;
        }

        private void ListViewEx_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= ListViewEx_Loaded;
            _scrollViewer = this.FindFirstChild<ScrollViewer>();
            if (_scrollViewer != null)
                _scrollViewer.ViewChanged += Sv_ViewChanged;
            _panel = ItemsPanelRoot as ItemsStackPanel;
        }

        private async void Sv_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (_scrollViewer.VerticalOffset < 1)
            {
                var source = ItemsSource as ISupportReversalLoading;
                if (source?.HasMoreItems == true)
                    await source.LoadMoreItemsAsync(1);
            }
            FirstVisibleIndex = _panel.FirstVisibleIndex;
        }
    }
}
