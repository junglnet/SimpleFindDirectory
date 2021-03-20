using Bochky.FindDirectory.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bochky.FindDirectory.Common.Interfaces
{
    public interface ILoadFolderList
    {
        Task<IEnumerable<Folder>> LoadDirectoriesAsync();
    }
}
