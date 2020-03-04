using System;
using System.Threading.Tasks;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using Bochky.FindDirectory.Service.Core;

namespace Bochky.FindDirectory.Service.Implementation
{
    public class FindService : IFindServiceContract
    {
        private readonly SearchEngine _searchEngine;

        private readonly ILoadFindFOlderService _loadFindFOlderService;
        public FindService()
        {
            
            _searchEngine = new SearchEngine();
            _loadFindFOlderService = new LoadFindFolderService();

        }
        public async Task<SearchResult> FindAsync(
            FindRequest findRequest,            
            bool isDeepSearch)
        {
                        
            try
            {
                var foldersToFinding = await _loadFindFOlderService.LoadDirectoriesAsync(Environment.CurrentDirectory + "\\" + "FindFolder.cfg");

                return await _searchEngine.FindAsync(findRequest, foldersToFinding, isDeepSearch);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

            
        }
    }
}
