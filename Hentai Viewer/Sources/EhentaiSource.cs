using System;
using System.IO;
using System.Net;
using System.Text;

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

        public async void Login()
        {
            var wrq = WebRequest.CreateHttp(new Uri("https://forums.e-hentai.org/index.php?act=Login&CODE=01"));
            wrq.CookieContainer = new CookieContainer();
            wrq.Headers["User-Agent"] = Settings.UAString;
            wrq.Method = "POST";
            string data = $"b=d&bt=pone&CookieDate=1&ipb_login_submit=Login%21&UserName={Username}&returntype=8&PassWord={Password}";
            wrq.Headers["Content-Length"] = Encoding.UTF8.GetByteCount(data).ToString();
            wrq.ContentType = "application/x-www-form-urlencoded";
            using (var sw = new StreamWriter(await wrq.GetRequestStreamAsync()))
            {
                sw.Write(data);
                sw.Flush();
            }
            using (var wrs = (HttpWebResponse)await wrq.GetResponseAsync())
            {
                foreach (Cookie cookie in wrq.CookieContainer.GetCookies(new Uri("http://e-hentai.org")))
                    System.Diagnostics.Debug.WriteLine(cookie);
            }
        }
    }
}
