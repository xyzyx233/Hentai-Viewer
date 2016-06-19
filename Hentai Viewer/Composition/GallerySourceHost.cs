using System.Collections.Generic;

namespace Meowtrix.HentaiViewer.Composition
{
    class GallerySourceHost
    {
        private GallerySourceHost() { }
        public static GallerySourceHost Instance { get; } = new GallerySourceHost();
        public IList<IGallery> Sources { get; } = new List<IGallery>();
    }
}
