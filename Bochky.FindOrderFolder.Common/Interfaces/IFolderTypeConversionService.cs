using Bochky.FindDirectory.Common.Entities;
using System.Collections.Generic;

namespace Bochky.FindDirectory.Common.Interfaces
{
    public interface IFolderTypeConversionService
    {

        IEnumerable<ChekedFolder> CompareAndConvertToChekedFolder(
            IEnumerable<ChekedFolder> ClientFolderList, 
            IEnumerable<Folder> ServerFolderList);

        IEnumerable<Folder> ConvertToFolder(
            IEnumerable<ChekedFolder> ClientFolderList);


    }
}
