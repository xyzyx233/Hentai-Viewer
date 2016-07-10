using Windows.UI.Xaml.Controls;

namespace Meowtrix.HentaiViewer.ViewModels
{
    public class DetailPage : ViewPage
    {
        public override Symbol HeaderIcon => Symbol.Library;
        public GalleryEntryInfo Info { get; set; }
    }
}
