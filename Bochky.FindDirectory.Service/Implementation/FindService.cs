using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using Bochky.FindDirectory.Service.Core;

namespace Bochky.FindDirectory.Service.Implementation
{
    public class FindService : IFindServiceContract
    {
        private readonly IServiceFactoryServer _serviceFactoryServer;

       
        public FindService()
        {

            _serviceFactoryServer = AppServiceFactory.Current;

        }
        public async Task<SearchResult> FindAsync(
            FindRequest findRequest,
            IEnumerable<Folder> foldersToFinding,
            bool isDeepSearch)
        {
                        
            try
            {
                
                return 
                    await _serviceFactoryServer.SearchEngine.FindAsync(
                        findRequest, 
                        foldersToFinding, 
                        isDeepSearch);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

            
        }

        public async Task<IEnumerable<Folder>> LoadDirectoriesAsync()
        {
            try
            {

                return 
                    await _serviceFactoryServer
                        .LoadFindFolderService
                        .LoadDirectoriesAsync("FindPoint.xml");

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }
    }
}
