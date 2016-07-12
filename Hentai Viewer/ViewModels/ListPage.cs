using System.Collections.ObjectModel;
using Meowtrix.HentaiViewer.Composition;
using Windows.UI.Xaml.Controls;

namespace Meowtrix.HentaiViewer.ViewModels
{
    public class ListPage : ViewPage
    {
        public override Symbol HeaderIcon => Symbol.List;
        public ObservableCollection<GalleryEntryInfo> Entries { get; } = new ObservableCollection<GalleryEntryInfo>();
        public ListPage(SearchResult searchResult)
        {
            Provider = searchResult.Provider;
            SearchInfo = searchResult.SearchInfo;
            TotalPages = searchResult.PagesCount;
            CurrentPage = 1;
            foreach (var entry in searchResult.Entries)
                Entries.Add(entry);
        }

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

        #region TotalPages
        private int _totalpages;
        public int TotalPages
        {
            get { return _totalpages; }
            set
            {
                if (_totalpages != value)
                {
                    _totalpages = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public IGallery Provider { get; }
        public SearchInfo SearchInfo { get; }
        internal int frompage = 1, topage = 1;
    }
}
