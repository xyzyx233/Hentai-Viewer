using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Meowtrix.HentaiViewer.Controls
{
    public interface ISupportReversalLoading
    {
        bool HasMoreItems { get; }
        Task<LoadMoreItemsResult> LoadMoreItemsAsync(uint count);
    }
}
