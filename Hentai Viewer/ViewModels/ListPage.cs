using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace Meowtrix.HentaiViewer.ViewModels
{
    public class ListPage : ViewPage
    {
        public override Symbol HeaderIcon => Symbol.List;
        public ObservableCollection<GalleryEntryInfo> Entries { get; } = new ObservableCollection<GalleryEntryInfo>();

        #region CurrentPage
        private int _currentpage;
        public int CurrentPage
        {
            get { return _currentpage; }
            set
            {
                if (_currentpage != value)
                {
                    _currentpage = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public int TotalPages { get; set; }
    }
}
