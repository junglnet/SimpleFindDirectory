using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bochky.FindOrderFolder.Logic;
using Bochky.FindOrderFolder.Common;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bochky.FindOrderFolder.Tests
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

            var findEngle = new SearchEngine(await LoadFindFolderService.LoadDirectoriesAsync(Environment.CurrentDirectory + "\\" + "Test.txt"));

            var findRequest1 = new FindRequest("32446");

            var searchResult = await findEngle.FindAsync(findRequest1, false);

            Assert.AreEqual(1, searchResult.FindDirectories.Count);

        }

        [TestMethod]
        public async Task LoopSearchTest()
        {

            var findEngle = new SearchEngine(await LoadFindFolderService.LoadDirectoriesAsync(Environment.CurrentDirectory + "\\" + "FindFolder.cfg"));

            var findRequest1 = new FindRequest("Поляк");

            List<SearchResult> summaryResult = new List<SearchResult>();

            for (int i= 0; i < 1000; i ++) {
                
                var searchResult = await findEngle.FindAsync(findRequest1, false);

                summaryResult.Add(searchResult);

            }

            foreach(var item in summaryResult)
            {
                Assert.AreEqual(3, item.FindDirectories.Count);
            }
            

        }

    }
}
