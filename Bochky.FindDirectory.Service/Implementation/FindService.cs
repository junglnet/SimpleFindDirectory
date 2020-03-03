using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using Bochky.FindDirectory.Service.Core;


namespace Bochky.FindDirectory.Service.Implementation
{
    public class FindService : IFindService
    {
        private readonly SearchEngine _searchEngine;
        public FindService()
        {
            
            _searchEngine = new SearchEngine();

        }
        public async Task<SearchResult> FindAsync(
            FindRequest findRequest,            
            bool isDeepSearch)
        {
                        
            try
            {
                var foldersToFinding = await LoadFindFolderService.LoadDirectoriesAsync(Environment.CurrentDirectory + "\\" + "FindFolder.cfg");

                return await _searchEngine.FindAsync(findRequest, foldersToFinding, isDeepSearch);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

            
        }
    }
}
