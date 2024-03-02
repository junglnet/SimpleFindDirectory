using System.Collections.Generic;
using System.Threading.Tasks;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using RemoteServiceExtension;

namespace Bochky.FindDirectory.Implementation
{
    public class FindServiceClient : IFindServiceContract
    {

        private readonly IClientChannelFactory<IFindServiceContract> _channelFactory;

        public FindServiceClient()
        {
            _channelFactory = new ClientChannelFactory<IFindServiceContract>("FindService");           
        }

        public async Task<SearchResult> FindAsync(
            FindRequest findRequest, 
            IEnumerable<Folder> foldersToFinding, 
            bool isDeepSearch) =>
             await RemoteServiceCall<IFindServiceContract>.RemoteCall(_channelFactory, 
                    serv => serv.FindAsync(findRequest, foldersToFinding, isDeepSearch));

        public async Task<IEnumerable<Folder>> LoadDirectoriesAsync() =>
             await RemoteServiceCall<IFindServiceContract>.RemoteCall(_channelFactory,
                    serv => serv.LoadDirectoriesAsync());
    }
}
