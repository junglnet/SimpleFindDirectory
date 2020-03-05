using Bochky.FindDirectory.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bochky.FindDirectory.Common.Interfaces
{
    public interface ISaveChekedFolderListService
    {

        Task SaveChekedFolderList(IEnumerable<ChekedFolder> chekedFolders, string filePath);

    }
}
