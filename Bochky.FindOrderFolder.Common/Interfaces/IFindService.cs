using System.Threading;
using System.Threading.Tasks;
using Bochky.FindDirectory.Common.Entities;

namespace Bochky.FindDirectory.Common.Interfaces
{
    public interface IFindService
    {
        Task<SearchResult> FindAsync(
            FindRequest findRequest,
            bool isDeepSearch, CancellationToken token);
    }
}
