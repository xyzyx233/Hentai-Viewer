using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Meowtrix.HentaiViewer.ViewModels
{
    public abstract class ViewPage : NotificationObject
    {
        public abstract Symbol HeaderIcon { get; }
        public Uri Uri { get; set; }
        public virtual bool CanRefresh => true;
        public virtual Task RefreshAsync() => Task.CompletedTask;
    }
}
