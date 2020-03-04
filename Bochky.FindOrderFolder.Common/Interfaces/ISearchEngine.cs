using Bochky.FindDirectory.Common.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bochky.FindDirectory.Common.Interfaces
{
    public interface ISearchEngine
    {
        Task<SearchResult> FindAsync(
            FindRequest findRequest,
            IEnumerable<Folder> foldersToFinding,
            bool isDeepSearch,
            CancellationToken token = default);
    }
}
