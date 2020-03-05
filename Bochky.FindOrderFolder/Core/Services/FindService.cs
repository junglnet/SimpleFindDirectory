using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Interfaces;
using Bochky.FindDirectory.Common.Interfaces;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;

namespace Bochky.FindDirectory.Core.Services
{
    class FindService : IFindService
    {

        private readonly IFindServiceContract _findServiceContract;

        public FindService(IFindServiceContract findServiceContract)
        {
            _findServiceContract = findServiceContract;
        }
        

        public async Task<SearchResult> FindAsync(
            FindRequest findRequest, 
            IEnumerable<Folder> foldersToFinding, 
            bool isDeepSearch, 
            CancellationToken token) 
            => await _findServiceContract.FindAsync(
                findRequest, 
                foldersToFinding, 
                isDeepSearch)
                .WithCancellation(token)
                .WithTimeout(TimeSpan.FromSeconds(15));

        public async Task<IEnumerable<Folder>> LoadDirectoriesAsync(CancellationToken token)
            => await _findServiceContract.LoadDirectoriesAsync()
                .WithCancellation(token)
                .WithTimeout(TimeSpan.FromSeconds(15));
    }
}
