using System.Composition;

namespace Meowtrix.HentaiViewer.Sources
{
    [Export(typeof(Composition.IGallery))]
    class NHentaiSource : Composition.IGallery
    {
        public string Name => "NHentai";
    }
}
