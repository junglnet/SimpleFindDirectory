using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bochky.FindDirectory.Common.Entities;

namespace Bochky.FindDirectory.Common.Interfaces
{
    public interface IFindService
    {
        Task<SearchResult> FindAsync(
            FindRequest findRequest,
            IEnumerable<Folder> foldersToFinding,
            bool isDeepSearch, CancellationToken token = default);
        
        Task<IEnumerable<Folder>> LoadDirectoriesAsync(CancellationToken token = default);
    }
}
