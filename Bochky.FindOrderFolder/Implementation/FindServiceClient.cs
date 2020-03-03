using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using RemoteServiceExtension;
using Microsoft.VisualStudio.Threading;

namespace Bochky.FindDirectory.Implementation
{
    public class FindServiceClient : IFindServiceContract
    {

        private readonly IClientChannelFactory<IFindServiceContract> _channelFactory;

        public FindServiceClient()
        {
            _channelFactory = new ClientChannelFactory<IFindServiceContract>("FindService"); ;
        }

        public async Task<SearchResult> FindAsync(FindRequest findRequest, bool isDeepSearch) =>
             await RemoteServiceCall<IFindServiceContract>.RemoteCall(_channelFactory, 
                    serv => serv.FindAsync(findRequest, isDeepSearch));


    }
}
