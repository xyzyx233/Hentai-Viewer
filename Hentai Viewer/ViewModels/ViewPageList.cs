using System.Collections.ObjectModel;

namespace Meowtrix.HentaiViewer.ViewModels
{
    class ViewPageList
    {
        public ObservableCollection<ViewPage> Pages { get; } = new ObservableCollection<ViewPage>
        {
            Settings.Current.DefaultGallery.GetList()
        };
    }
}
