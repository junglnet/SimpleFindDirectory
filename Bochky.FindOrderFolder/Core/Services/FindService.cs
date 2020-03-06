using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using Bochky.FindDirectory.Common.Exceptions;

namespace Bochky.FindDirectory.Core.Services
{
    class FindService : IFindService
    {

        private readonly IFindServiceContract _findServiceContract;
        private readonly IFolderTypeConversionService _folderTypeConversionService;

        public FindService(
            IFindServiceContract findServiceContract,
            IFolderTypeConversionService folderTypeConversionService)
        {
            _findServiceContract = findServiceContract;
            _folderTypeConversionService = folderTypeConversionService;
        }
        

        public async Task<SearchResult> FindAsync(
            string request, 
            IEnumerable<ChekedFolder> foldersToFinding, 
            bool isDeepSearch, 
            CancellationToken token = default)
        {

            if (request == null) throw new NullSearchRequestException();            

            return
                await _findServiceContract.FindAsync(
                new FindRequest(request),
                _folderTypeConversionService.ConvertToFolder(foldersToFinding),
                isDeepSearch)
                .WithCancellation(token)
                .WithTimeout(TimeSpan.FromSeconds(15));

        }
            

        public async Task<IEnumerable<Folder>> LoadDirectoriesAsync(CancellationToken token = default)
            => await _findServiceContract.LoadDirectoriesAsync()
                .WithCancellation(token)
                .WithTimeout(TimeSpan.FromSeconds(15));
    }
}
