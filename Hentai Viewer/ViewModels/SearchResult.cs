using System.Collections.Generic;
using Meowtrix.HentaiViewer.Composition;

namespace Meowtrix.HentaiViewer.ViewModels
{
    public struct SearchResult
    {
        public IGallery Provider { get; set; }
        public SearchInfo SearchInfo { get; set; }
        public ICollection<GalleryEntryInfo> Entries { get; set; }
        public int PagesCount { get; set; }
    }
}
