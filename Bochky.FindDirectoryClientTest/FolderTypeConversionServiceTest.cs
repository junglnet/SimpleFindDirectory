using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using Bochky.FindDirectory.Core.Services;
using System.Collections.Generic;

namespace Bochky.FindDirectoryClientTest
{
    [TestClass]
    public class FolderTypeConversionServiceTest
    {
        [TestMethod]
        public void CompareAndConvertToChekedFolderTest()
        {

            var clientList = new List<ChekedFolder>() {

                new ChekedFolder(new Folder("test1"), true),
                new ChekedFolder(new Folder("test2"), true),
                new ChekedFolder(new Folder("test3"), false),
                new ChekedFolder(new Folder("test4"), false),
                new ChekedFolder(new Folder("test5"), true),

            };

            var serverList = new List<Folder>() {
                
                new Folder("Test1"),
                new Folder("Test2"),                
                new Folder("Test4"),
                new Folder("Test7"),
                new Folder("Test8"),

            };

            var sampleResult1 = new List<ChekedFolder>() {

                new ChekedFolder(new Folder("test1"), true),
                new ChekedFolder(new Folder("test2"), true),                
                new ChekedFolder(new Folder("test4"), false),
                new ChekedFolder(new Folder("test7"), true),
                new ChekedFolder(new Folder("test8"), true),


            };

            IFolderTypeConversionService folderTypeConversionService = new FolderTypeConversionService();

            var resultClientList = folderTypeConversionService.CompareAndConvertToChekedFolder(clientList, serverList);

            var result = sampleResult1.Except(resultClientList);

            Assert.AreEqual(result.Count(), 0);

        }

        [TestMethod]
        public void ConvertToFolderTest()
        {

            var clientList = new List<ChekedFolder>() {

                new ChekedFolder(new Folder("test1"), true),
                new ChekedFolder(new Folder("test2"), true),
                new ChekedFolder(new Folder("test3"), false),
                new ChekedFolder(new Folder("test4"), false),
                new ChekedFolder(new Folder("test5"), true),

            };

            IFolderTypeConversionService folderTypeConversionService = new FolderTypeConversionService();

            var resultClientList = folderTypeConversionService.ConvertToFolder(clientList);

            Assert.AreEqual(resultClientList.Count(), 3);
        }
    }
}
