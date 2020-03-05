using System.Collections.Generic;
using System.Threading.Tasks;
using Bochky.FindDirectory.Common.Entities;


namespace Bochky.FindDirectory.Common.Interfaces
{
    public interface ISyncSearchFolderConfigurationService
    {

        Task<IEnumerable<ChekedFolder>> Pull();

    }
}
