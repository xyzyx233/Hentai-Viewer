using System;
using System.IO;
using System.Net;
using System.Text;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;

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

        #region IsLoginEnabled
        private bool _isloginenabled = true;
        public bool IsLoginEnabled
        {
            get { return _isloginenabled; }
            set
            {
                if (_isloginenabled != value)
                {
                    _isloginenabled = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region IsLogin
        private bool _islogin;
        public bool IsLogin
        {
            get { return _islogin; }
            set
            {
                if (_islogin != value)
                {
                    _islogin = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public string ipb_member_id { get; private set; }
        public string ipb_passhash { get; private set; }
        public async void Login()
        {
            IsLoginEnabled = false;
            var resources = new ResourceLoader();
            try
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
                    ipb_member_id = ipb_passhash = null;
                    foreach (Cookie cookie in wrq.CookieContainer.GetCookies(new Uri("http://e-hentai.org")))
                        if (cookie.Name == "ipb_member_id")
                            ipb_member_id = cookie.Value;
                        else if (cookie.Name == "ipb_pass_hash")
                            ipb_passhash = cookie.Value;
                    if (ipb_member_id == null || ipb_passhash == null)//login fail
                    {
                        string html;
                        using (var reader = new StreamReader(wrs.GetResponseStream()))
                            html = reader.ReadToEnd();
                        string prestring = "The following errors were found:</div>\n\t<div class=\"tablepad\"><span class=\"postcolor\">";
                        html = html.Substring(html.IndexOf(prestring) + prestring.Length);
                        html = html.Substring(0, html.IndexOf("</span>"));

                        var dialog = new MessageDialog(html, resources.GetString("LoginFail"));
                        dialog.Commands.Add(new UICommand(resources.GetString("OK"), _ => { }));
                        await dialog.ShowAsync();
                        IsLogin = false;
                    }
                    else IsLogin = true;
                }
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog(ex.GetBaseException().Message, resources.GetString("LoginFail"));
                dialog.Commands.Add(new UICommand(resources.GetString("OK"), _ => { }));
                await dialog.ShowAsync();
            }
            finally
            {
                IsLoginEnabled = true;
            }
        }
    }
}
