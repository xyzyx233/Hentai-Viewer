using System.Collections.Generic;
using Meowtrix.HentaiViewer.Sources;

namespace Meowtrix.HentaiViewer.Composition
{
    class GallerySourceHost
    {
        private GallerySourceHost() { }
        public static GallerySourceHost Instance { get; } = new GallerySourceHost();
        public IList<IGallery> Sources { get; } = new List<IGallery>
        {
            new EhentaiSource(),
            new NHentaiSource()
        };
    }
}
