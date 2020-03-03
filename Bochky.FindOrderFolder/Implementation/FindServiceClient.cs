using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using RemoteServiceExtension;

namespace Bochky.FindDirectory.Implementation
{
    public class FindServiceClient : IFindService
    {

        private readonly IClientChannelFactory<IFindService> _channelFactory;

        public FindServiceClient()
        {
            _channelFactory = new ClientChannelFactory<IFindService>("FindService"); ;
        }

        public async Task<SearchResult> FindAsync(FindRequest findRequest, bool isDeepSearch) =>
             await RemoteServiceCall<IFindService>.RemoteCall(_channelFactory, 
                    serv => serv.FindAsync(findRequest, isDeepSearch));


    }
}
