using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Meowtrix.HentaiViewer.Composition;
using Meowtrix.ITask;

namespace Meowtrix.HentaiViewer.ViewModels
{
    class ViewPageList : NotificationObject
    {
        public ObservableCollection<ViewPage> Pages { get; } = new ObservableCollection<ViewPage>();

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
                    OnPropertyChanged(nameof(SelectedPage));
                }
            }
        }
        #endregion

        public ViewPage SelectedPage => Pages[SelectedIndex];
        public ViewPageList()
        {
            AddPageAsync(NewSearchPageAsync(Settings.Current.DefaultGallery, new SearchInfo()).AsITask());
        }
        public async void AddPageAsync(ITask<ViewPage> task)
        {
            int position = Pages.Count;
            Pages.Add(new PlaceHolderPage());
            var result = await task;
            Pages[position] = result;
        }
        public async Task<ListPage> NewSearchPageAsync(IGallery provider, SearchInfo searchInfo)
            => new ListPage(await provider.SearchAsync(searchInfo));
    }
}
