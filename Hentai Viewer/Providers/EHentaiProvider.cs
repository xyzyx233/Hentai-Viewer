using System;
using System.Composition;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Meowtrix.HentaiViewer.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.Web.Http;

namespace Meowtrix.HentaiViewer.Providers
{
    [Export(typeof(Composition.IGallery))]
    class EHentaiProvider : Composition.IGallery
    {
        public EHentaiProvider()
        {
            HttpHost.CookieManager.SetCookie(new HttpCookie(nameof(uconfig), "e-hentai.org", "") { Value = uconfig });
            HttpHost.CookieManager.SetCookie(new HttpCookie(nameof(uconfig), "exhentai.org", "") { Value = uconfig });
        }
        private const string uconfig = "uh_y-lt_m-tl_r-tr_2-ts_m-prn_y-dm_t-ar_0-xns_0-rc_0-rx_0-ry_0-cs_a-fs_p-to_a-pn_0-sc_0-ru_rrggb-xr_a-cats_0-ms_n-mt_n-sa_y-oi_n-qb_n-tf_n-hp_-hk_-xl_";
        public string Name => "EHentai";
        public UIElement SettingPage => new EHentaiSetting { Settings = SettingInstance };
        public static EHentaiSettings SettingInstance { get; } = new EHentaiSettings();
        public void LoadSettings(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata) => SettingInstance.Load(localdata, roamingdata);
        public void SaveSettings(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata) => SettingInstance.Save(localdata, roamingdata);
        public Task<ListPage> GetListAsync(SearchInfo info)
        {
            throw new NotImplementedException();
        }
    }
    class EHentaiSettings : NotificationObject
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

        #region NickName
        private string _nickname;
        public string NickName
        {
            get { return _nickname; }
            set
            {
                if (_nickname != value)
                {
                    _nickname = value;
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
                    //TODO:Update HttpClient
                }
            }
        }
        #endregion

        public string ipb_member_id { get; private set; }
        public string ipb_pass_hash { get; private set; }
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
                    ipb_member_id = ipb_pass_hash = null;
                    foreach (Cookie cookie in wrq.CookieContainer.GetCookies(new Uri("http://e-hentai.org")))
                        if (cookie.Name == nameof(ipb_member_id))
                            ipb_member_id = cookie.Value;
                        else if (cookie.Name == nameof(ipb_pass_hash))
                            ipb_pass_hash = cookie.Value;
                    string html;
                    using (var reader = new StreamReader(wrs.GetResponseStream()))
                        html = reader.ReadToEnd();
                    if (ipb_member_id == null || ipb_pass_hash == null)//login fail
                    {
                        string prestring = "The following errors were found:</div>\n\t<div class=\"tablepad\"><span class=\"postcolor\">";
                        html = html.Substring(html.IndexOf(prestring) + prestring.Length);
                        html = html.Substring(0, html.IndexOf("</span>"));

                        var dialog = new MessageDialog(html, resources.GetString("LoginFail"));
                        dialog.Commands.Add(new UICommand(resources.GetString("OK"), _ => { }));
                        await dialog.ShowAsync();
                        IsLogin = false;
                    }
                    else
                    {
                        string prestring = "You are now logged in as: ";
                        html = html.Substring(html.IndexOf(prestring) + prestring.Length);
                        NickName = html.Substring(0, html.IndexOf("<br"));
                        IsLogin = true;
                    }
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
        public void Logout() => IsLogin = false;
        public void Load(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata)
        {
            var login = (ApplicationDataCompositeValue)localdata.Values["Login"];
            if (login != null)
            {
                IsLogin = (bool)login[nameof(IsLogin)];
                Username = (string)login[nameof(Username)];
                if (IsLogin)
                {
                    NickName = (string)login[nameof(NickName)];
                    ipb_member_id = (string)login[nameof(ipb_member_id)];
                    ipb_pass_hash = (string)login[nameof(ipb_pass_hash)];
                }
            }
        }
        public void Save(ApplicationDataContainer localdata, ApplicationDataContainer roamingdata)
        {
            var login = new ApplicationDataCompositeValue();
            login[nameof(IsLogin)] = IsLogin;
            login[nameof(Username)] = Username;
            if (IsLogin)
            {
                login[nameof(NickName)] = NickName;
                login[nameof(ipb_member_id)] = ipb_member_id;
                login[nameof(ipb_pass_hash)] = ipb_pass_hash;
            }
            localdata.Values["Login"] = login;
        }
    }
}
