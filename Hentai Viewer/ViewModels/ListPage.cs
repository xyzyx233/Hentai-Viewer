using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Meowtrix.HentaiViewer.Composition;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Meowtrix.HentaiViewer.ViewModels
{
    public class ListPage : ViewPage
    {
        public override Symbol HeaderIcon => Symbol.List;
        private GalleryEntryCollection _entries;
        public IList<GalleryEntryInfo> Entries => _entries;
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

        public async void JumpAsync()
        {
            var dialog = new SelectPageDialog { CurrentPage = this.CurrentPage, TotalPages = this.TotalPages };
            if (await dialog.ShowAsync() == ContentDialogResult.Secondary) return;
            int destpage;
            if (int.TryParse(dialog.Input, out destpage) && destpage > 0 && destpage <= TotalPages)
            {
                CurrentPage = destpage;
                if (destpage >= frompage && destpage <= topage)
                    SelectedIndex = (destpage - frompage) * itemsPerPage;
                else
                {
                    int index = Container.SelectedIndex;
                    Container.Pages[index] = new PlaceHolderPage();
                    await RefreshAsync();
                    Container.Pages[index] = this;
                }
            }
            else
            {
                var res = new ResourceLoader();
                var msg = new MessageDialog(res.GetString("InvalidPageNumber"));
                await msg.ShowAsync();
            }
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

        #region SelectedIndex
        private int _selectedindex;
        public int SelectedIndex
        {
            get { return _selectedindex; }
            set
            {
                if (_selectedindex != value)
                {
                    _selectedindex = value;
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
