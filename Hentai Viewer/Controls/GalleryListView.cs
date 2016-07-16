using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Meowtrix.HentaiViewer.Controls
{
    public class GalleryListView : ListView
    {
        private ScrollViewer _scrollViewer;

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
        }

        private async void Sv_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (e.IsIntermediate == false && _scrollViewer.VerticalOffset < 1)
            {
                var source = ItemsSource as ISupportReversalLoading;
                if (source?.HasMoreItems == true)
                    await source.LoadMoreItemsAsync(1);
            }
        }
    }
}
