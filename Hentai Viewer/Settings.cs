using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;

namespace Meowtrix.HentaiViewer
{
    class Settings : NotificationObject
    {
        private Settings() { }
        public static Settings Current { get; } = new Settings();
        public void Load() => LoadAsync().Wait();
        public async Task LoadAsync()
        {
            if (StorageApplicationPermissions.FutureAccessList.ContainsItem("StorageFolder"))
                _folder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("StorageFolder", AccessCacheOptions.DisallowUserInput | AccessCacheOptions.FastLocationsOnly);
            else
            {
                _folder = KnownFolders.SavedPictures;
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("StorageFolder", _folder);
            }
            var localsettings = ApplicationData.Current.LocalSettings;
            object tempval;
            if (localsettings.Values.TryGetValue("GroupByAuthor", out tempval))
                _groupbyauthor = (bool)tempval;
        }
        public void Save()
        {
            var localsettings = ApplicationData.Current.LocalSettings;
            localsettings.Values["GroupByAuthor"] = _groupbyauthor;
        }

        private StorageFolder _folder;
        public string StorageFolder => _folder.Path;

        #region GroupByAuthor
        private bool _groupbyauthor;
        public bool GroupByAuthor
        {
            get { return _groupbyauthor; }
            set
            {
                if (_groupbyauthor != value)
                {
                    _groupbyauthor = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public async void ChangeStorageFolder()
        {
            var picker = new FolderPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            StorageFolder folder = await picker.PickSingleFolderAsync();
            if (folder != null)
            {
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("StorageFolder", folder);
                _folder = folder;
                OnPropertyChanged(nameof(StorageFolder));
            }
        }
    }
}
