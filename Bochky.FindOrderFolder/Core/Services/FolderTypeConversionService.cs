using System;
using System.Collections.Generic;
using System.Linq;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;

namespace Bochky.FindDirectory.Core.Services
{
    public class FolderTypeConversionService : IFolderTypeConversionService
    {
        public IEnumerable<ChekedFolder> CompareAndConvertToChekedFolder(
            IEnumerable<ChekedFolder> ClientFolderList, 
            IEnumerable<Folder> ServerFolderList)
        {

            List<ChekedFolder> resultChekedFolders = new List<ChekedFolder>();

            var clientConvertedFolder = ClientFolderList.Select(item => item.Folder);

            var foldersToAdd = ServerFolderList.Except(clientConvertedFolder);

            resultChekedFolders.AddRange(
                foldersToAdd.Select(
                    item => new ChekedFolder(item, true)));

            var foldersToDelete = clientConvertedFolder.Except(ServerFolderList);

            resultChekedFolders.AddRange(
                ClientFolderList.Except(
                    foldersToDelete.Select(
                        item => new ChekedFolder(item, true))));

            return resultChekedFolders.OrderBy(item => item.Folder.DirectoryPath);

        }

        public IEnumerable<Folder> ConvertToFolder(IEnumerable<ChekedFolder> ClientFolderList)
            => ClientFolderList.Where(item => item.IsCheked).Select(item => item.Folder);



    }
}
