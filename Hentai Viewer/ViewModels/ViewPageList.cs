using System.Collections.ObjectModel;
using Meowtrix.ITask;

namespace Meowtrix.HentaiViewer.ViewModels
{
    class ViewPageList
    {
        public ObservableCollection<ViewPage> Pages { get; } = new ObservableCollection<ViewPage>();
        public ViewPageList()
        {
            AddPageAsync(Settings.Current.DefaultGallery.GetListAsync().AsITask());
        }
        public async void AddPageAsync(ITask<ViewPage> task)
        {
            int position = Pages.Count;
            Pages.Add(new PlaceHolderPage());
            var result = await task;
            Pages[position] = result;
        }
    }
}
