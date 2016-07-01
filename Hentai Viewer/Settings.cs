﻿using System;
using System.Linq;
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
        public const string UAString = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
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
            var roamingsettings = ApplicationData.Current.RoamingSettings;
            object tempval;
            if (localsettings.Values.TryGetValue(nameof(GroupByAuthor), out tempval))
                _groupbyauthor = (bool)tempval;
            if (roamingsettings.Values.TryGetValue(nameof(DefaultGallery), out tempval))
                DefaultGalleryName = (string)tempval;
            else DefaultGalleryName = string.Empty;
            foreach (var gallery in Composition.GallerySourceHost.Instance.Sources)
                gallery.LoadSettings(localsettings.CreateContainer(gallery.Name, ApplicationDataCreateDisposition.Always),
                    roamingsettings.CreateContainer(gallery.Name, ApplicationDataCreateDisposition.Always));
        }
        public void Save()
        {
            var localsettings = ApplicationData.Current.LocalSettings;
            var roamingsettings = ApplicationData.Current.RoamingSettings;
            localsettings.Values[nameof(GroupByAuthor)] = _groupbyauthor;
            roamingsettings.Values[nameof(DefaultGallery)] = DefaultGalleryName;
            foreach (var gallery in Composition.GallerySourceHost.Instance.Sources)
                gallery.SaveSettings(localsettings.CreateContainer(gallery.Name, ApplicationDataCreateDisposition.Always),
                    roamingsettings.CreateContainer(gallery.Name, ApplicationDataCreateDisposition.Always));
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
        public string[] GallerySources { get; } = Composition.GallerySourceHost.Instance.Sources.Select(x => x.Name).ToArray();

        #region DefaultGallery
        public int DefaultGalleryIndex
        {
            get { return Composition.GallerySourceHost.Instance.Sources.IndexOf(DefaultGallery); }
            set { DefaultGallery = Composition.GallerySourceHost.Instance.Sources[value]; }
        }
        public string DefaultGalleryName
        {
            get { return DefaultGallery.Name; }
            set
            {
                DefaultGallery = Composition.GallerySourceHost.Instance.Sources.FirstOrDefault(x => x.Name == value) ??
                  Composition.GallerySourceHost.Instance.Sources[0];
            }
        }
        public Composition.IGallery DefaultGallery { get; private set; }
        #endregion
    }
}
