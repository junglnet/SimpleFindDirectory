using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bochky.FindDirectory.Logic;
using Bochky.FindDirectory.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using Bochky.FindDirectory.Service.Core;
using Bochky.FindDirectory.Common.Entities;

namespace Bochky.FindDirectory.Tests
{
    [TestClass]
    public class UnitTest1
    {
       //  [TestMethod]
        public async Task LoadFindFolderServiceTest()
        {

          //  var loadFindFolderService = new LoadFindFolderService();

            var result = await LoadFindFolderService.LoadDirectoriesAsync(Environment.CurrentDirectory + "\\" + "FindFolder.cfg");

            Assert.AreEqual(2, result.Count);

        }
        
       // [TestMethod]
        public async Task FindnewTest()
        {

            var findEngle = new SearchEngine();

            var findRequest1 = new FindRequest();

            var searchResult = await findEngle.FindAsync(findRequest1, await LoadFindFolderService.LoadDirectoriesAsync(Environment.CurrentDirectory + "\\" + "FindFolder.cfg"), false);

            //Assert.AreEqual(1, searchResult.FindDirectories Count);

        }

        [TestMethod]
        public async Task LoopSearchTest()
        {

            var findEngle = new SearchEngine();

            var findRequest1 = new FindRequest();

            List<SearchResult> summaryResult = new List<SearchResult>();

            for (int i= 0; i < 1000; i ++) {
                
                var searchResult = await findEngle.FindAsync(findRequest1, await LoadFindFolderService.LoadDirectoriesAsync(Environment.CurrentDirectory + "\\" + "FindFolder.cfg"), false);

                summaryResult.Add(searchResult);

            }

            foreach(var item in summaryResult)
            {
              //  Assert.AreEqual(3, item.FindDirectories.Count);
            }
            

        }

    }
}
