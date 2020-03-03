using Bochky.FindDirectory.Common.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Collections.Generic;

namespace Bochky.FindDirectory.Common.Interfaces
{
    [ServiceContract]
    public interface IFindService
    {
        [OperationContract]
        Task<SearchResult> FindAsync(
            FindRequest findRequest,            
            bool isDeepSearch);
    }
}
