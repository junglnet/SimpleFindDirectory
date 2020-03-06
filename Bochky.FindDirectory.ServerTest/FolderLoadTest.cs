using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using Bochky.FindDirectory.Service.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Bochky.FindDirectoryServerTest
{
    [TestClass]
    public class FolderLoadTest
    {
        [TestMethod]
        public async Task FolderLoad()
        {

            var serverList = new List<Folder>() {

                new Folder(@"\\fileserver\Bochky\1. Клиенты\1. Заказчики", "Клиенты. Заказчики"),
                new Folder(@"\\fileserver\Bochky\1. Клиенты\2. Потенциальные клиенты", "Потенциальные клиенты"),
                new Folder(@"\\fileserver\Bochky\1. Клиенты\2. Потенциальные клиенты\_Заведенные", "Потенциальные Клиенты. Заведенные"),
                new Folder(@"\\fileserver\Bochky\1. Клиенты\2. Потенциальные клиенты\_На заведение", "Потенциальные Клиенты. На заведение"),
                new Folder(@"\\fileserver\Bochky\1. Клиенты\0. Архив\Потециальные", "Архив. Потециальные"),
                new Folder(@"\\fileserver\Bochky\1. Клиенты\0. Архив\Заказчики", "Архив. Заказчики"),


            };


            XmlSerializer formatter = new XmlSerializer(typeof(Folder[]));

            using (FileStream fs = new FileStream("FindPoint.xml", FileMode.Create))
            {
                await Task.Run(() => formatter.Serialize(fs, serverList.ToArray()));
            }



            ILoadFindFolderService loadFindFolderService = new LoadFindFolderFromXMLService("FindPoint.xml");

            var loadedChekedFolder = await loadFindFolderService.LoadDirectoriesAsync();

            Assert.AreEqual(loadedChekedFolder.Count(), 6);
        }
    }
}
