using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Meowtrix.HentaiViewer.Controls;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Meowtrix.HentaiViewer.ViewModels
{
    internal class GalleryEntryCollection : ObservableCollection<GalleryEntryInfo>, ISupportIncrementalLoading, ISupportReversalLoading
    {
        public ListPage Page { get; }
        public GalleryEntryCollection(ListPage page, IEnumerable<GalleryEntryInfo> entries) : base(entries)
        {
            Page = page;
        }
        bool ISupportIncrementalLoading.HasMoreItems => Page.topage < Page.TotalPages;
        bool ISupportReversalLoading.HasMoreItems => Page.frompage > 1;
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
        async Task<LoadMoreItemsResult> ISupportReversalLoading.LoadMoreItemsAsync(uint count) => new LoadMoreItemsResult { Count = await LoadPreviousPageAsync() };
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
