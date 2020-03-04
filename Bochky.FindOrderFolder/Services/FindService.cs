using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Interfaces;
using Bochky.FindDirectory.Common.Interfaces;
using Microsoft.VisualStudio.Threading;
using System;

namespace Bochky.FindDirectory.Services
{
    class FindService : IFindService
    {

        private readonly IFindServiceContract _findServiceContract;

        public FindService(IFindServiceContract findServiceContract)
        {
            _findServiceContract = findServiceContract;
        }
        

        public Task<SearchResult> FindAsync(FindRequest findRequest, bool isDeepSearch, CancellationToken token) 
            => _findServiceContract.FindAsync(findRequest, isDeepSearch).WithCancellation(token).WithTimeout(TimeSpan.FromSeconds(15));
        
    }
}
