using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Meowtrix.HentaiViewer.ViewModels
{
    internal class GalleryEntryCollection : ObservableCollection<GalleryEntryInfo>, ISupportIncrementalLoading
    {
        public ListPage Page { get; }
        public GalleryEntryCollection(ListPage page)
        {
            Page = page;
        }
        public bool HasMoreItems => Page.topage < Page.TotalPages;
        public void AddRange(IEnumerable<GalleryEntryInfo> items)
        {
            foreach (var item in items) Add(item);
        }
        public void PushRange(IEnumerable<GalleryEntryInfo> items)
        {
            int i = 0;
            foreach (var item in items)
                InsertItem(i++, item);
        }
        IAsyncOperation<LoadMoreItemsResult> ISupportIncrementalLoading.LoadMoreItemsAsync(uint count)
            => LoadMoreItemsCoreAsync().AsAsyncOperation();
        private async Task<LoadMoreItemsResult> LoadMoreItemsCoreAsync() => new LoadMoreItemsResult { Count = await LoadNextPageAsync() };
        public async Task<uint> LoadNextPageAsync()
        {
            var searchresult = await Page.Provider.SearchAsync(Page.SearchInfo, Page.topage + 1);
            Page.topage++;
            AddRange(searchresult.Entries);
            if (Page.itemsPerPage < searchresult.Entries.Count) Page.itemsPerPage = searchresult.Entries.Count;
            return (uint)searchresult.Entries.Count;
        }
        public async Task<uint> LoadPreviousPageAsync()
        {
            var searchresult = await Page.Provider.SearchAsync(Page.SearchInfo, Page.frompage - 1);
            Page.frompage--;
            PushRange(searchresult.Entries);
            if (Page.itemsPerPage < searchresult.Entries.Count) Page.itemsPerPage = searchresult.Entries.Count;
            return (uint)searchresult.Entries.Count;
        }
    }
}
