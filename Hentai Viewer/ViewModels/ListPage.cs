using System.Collections.Generic;
using System.Threading.Tasks;
using Meowtrix.HentaiViewer.Composition;
using Windows.UI.Xaml.Controls;

namespace Meowtrix.HentaiViewer.ViewModels
{
    public class ListPage : ViewPage
    {
        public override Symbol HeaderIcon => Symbol.List;
        private GalleryEntryCollection _entries;
        public ICollection<GalleryEntryInfo> Entries => _entries;
        public ListPage(SearchResult searchResult)
        {
            Provider = searchResult.Provider;
            SearchInfo = searchResult.SearchInfo;
            TotalPages = searchResult.PagesCount;
            CurrentPage = 1;
            _entries = new GalleryEntryCollection(this, searchResult.Entries);
        }

        public override async Task RefreshAsync()
        {
            frompage = topage = CurrentPage;
            var searchResult = await Provider.SearchAsync(SearchInfo, CurrentPage);
            TotalPages = searchResult.PagesCount;
            _entries = new GalleryEntryCollection(this, searchResult.Entries);
            OnPropertyChanged(nameof(Entries));
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
        internal int frompage = 1, topage = 1, itemsPerPage = 1;
    }
}
