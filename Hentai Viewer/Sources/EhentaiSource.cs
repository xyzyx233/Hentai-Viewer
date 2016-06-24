namespace Meowtrix.HentaiViewer.Sources
{
    class EhentaiSource : Composition.IGallery
    {
        public string Name => "EHentai";
        public static EhentaiSettings SettingInstance => Settings.Current.EhentaiSettings;
    }
    class EhentaiSettings : NotificationObject
    {
        #region Username
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Password
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public void Login()
        {

        }
    }
}
