using Windows.Storage;

namespace Meowtrix.HentaiViewer.Providers
{
    class NHentaiSettings : NotificationObject
    {
        #region UseHTTPS
        private bool _usehttps;
        public bool UseHTTPS
        {
            get { return _usehttps; }
            set
            {
                if (_usehttps != value)
                {
                    _usehttps = value;
                    OnPropertyChanged();
                    Scheme = value ? "https:" : "http:";
                }
            }
        }
        #endregion

        public string Scheme { get; private set; } = "http:";
        public void Load(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata)
        {
            object tempval;
            if (localdata.Values.TryGetValue(nameof(UseHTTPS), out tempval))
                UseHTTPS = (bool)tempval;
        }
        public void Save(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata)
        {
            localdata.Values[nameof(UseHTTPS)] = UseHTTPS;
        }
    }
}
