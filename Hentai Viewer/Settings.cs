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
            if (StorageApplicationPermissions.FutureAccessList.ContainsItem(nameof(StorageFolder)))
                _folder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(nameof(StorageFolder), AccessCacheOptions.DisallowUserInput | AccessCacheOptions.FastLocationsOnly);
            else
            {
                _folder = KnownFolders.SavedPictures;
                StorageApplicationPermissions.FutureAccessList.AddOrReplace(nameof(StorageFolder), _folder);
            }
            var localsettings = ApplicationData.Current.LocalSettings;
            object tempval;
            if (localsettings.Values.TryGetValue(nameof(GroupByAuthor), out tempval))
                _groupbyauthor = (bool)tempval;
        }
        public void Save()
        {
            var localsettings = ApplicationData.Current.LocalSettings;
            localsettings.Values[nameof(GroupByAuthor)] = _groupbyauthor;
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
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            StorageFolder folder = await picker.PickSingleFolderAsync();
            if (folder != null)
            {
                StorageApplicationPermissions.FutureAccessList.AddOrReplace(nameof(StorageFolder), folder);
                _folder = folder;
                OnPropertyChanged(nameof(StorageFolder));
            }
        }
        public Sources.EhentaiSettings EhentaiSettings { get; } = new Sources.EhentaiSettings();
    }
}
