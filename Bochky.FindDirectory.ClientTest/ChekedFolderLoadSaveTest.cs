using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using Bochky.FindDirectory.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bochky.FindDirectoryClientTest
{
    [TestClass]
    public class ChekedFolderLoadSaveTest
    {
        [TestMethod]
        public async Task ChekedFolderLoadSave()
        {
            var clientList = new List<ChekedFolder>() {

                new ChekedFolder(new Folder("test1"), true),
                new ChekedFolder(new Folder("test2"), true),
                new ChekedFolder(new Folder("test3"), false),
                new ChekedFolder(new Folder("test4"), false),
                new ChekedFolder(new Folder("test5"), true),

            };

            ILoadChekedFolderListService loadChekedFolderListService = new LoadChekedFolderListFromXMLService();
            
            ISaveChekedFolderListService saveChekedFolderListService = new SaveChekedFolderListToXMLService("test.xml");

            await saveChekedFolderListService.SaveChekedFolderList(clientList);

            var loadedChekedFolder = await loadChekedFolderListService.LoadChekedFolderList("test.xml");


            var result = clientList.Except(loadedChekedFolder);

            Assert.AreEqual(result.Count(), 0);
        }

    }
}
