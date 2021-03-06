﻿using Bochky.FindDirectory.Common.Entities;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Collections.Generic;

namespace Bochky.FindDirectory.Common.Interfaces
{
    [ServiceContract]
    public interface IFindServiceContract
    {
        [OperationContract]
        Task<SearchResult> FindAsync(
            FindRequest findRequest,
            IEnumerable<Folder> foldersToFinding,
            bool isDeepSearch);

        [OperationContract]
        Task<IEnumerable<Folder>> LoadDirectoriesAsync();
    }
}
