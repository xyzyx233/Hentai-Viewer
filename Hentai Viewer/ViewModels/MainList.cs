using System.Collections.ObjectModel;

namespace Meowtrix.HentaiViewer.ViewModels
{
    class MainList : NotificationObject
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
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }
        #endregion

        public ViewPage SelectedItem => Pages[SelectedIndex];
    }
}
