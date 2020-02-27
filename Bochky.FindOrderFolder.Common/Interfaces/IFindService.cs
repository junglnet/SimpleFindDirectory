using Bochky.FindOrderFolder.Common.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Bochky.FindOrderFolder.Common.Interfaces
{
    public interface IFindService
    {
        Task<SearchResult> FindAsync(
            FindRequest findRequest,
            bool isDeepSearch,
            CancellationToken token = default);
    }
}
