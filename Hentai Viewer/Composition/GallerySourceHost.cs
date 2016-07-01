using System.Collections.Generic;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;

namespace Meowtrix.HentaiViewer.Composition
{
    class GallerySourceHost
    {
        private GallerySourceHost()
        {
            var host = new ContainerConfiguration()
                .WithAssembly(typeof(GallerySourceHost).GetTypeInfo().Assembly)
                .CreateContainer();
            Sources = host.GetExports<IGallery>().ToArray();
        }
        public static GallerySourceHost Instance { get; } = new GallerySourceHost();
        public IList<IGallery> Sources { get; }
    }
}
