using System;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Meowtrix.HentaiViewer.ViewModels
{
    public class GalleryEntryInfo
    {
        public Uri Uri { get; set; }
        public string Title { get; set; }
        public Uri ThumbnailUri { get; set; }
        private ImageSource _thumbnail;
        public ImageSource Thumbnail => _thumbnail ?? (_thumbnail = new BitmapImage(ThumbnailUri));
    }
}
