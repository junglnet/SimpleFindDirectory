using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bochky.FindOrderFolder.Logic;
using Bochky.FindOrderFolder.Common;
using System.Threading.Tasks;

namespace Bochky.FindOrderFolder.Tests
{
    [TestClass]
    public class UnitTest1
    {
         [TestMethod]
        public async Task LoadFindFolderServiceTest()
        {

          //  var loadFindFolderService = new LoadFindFolderService();

            var result = await LoadFindFolderService.LoadDirectoriesAsync(Environment.CurrentDirectory + "\\" + "Test.txt");

            Assert.AreEqual(2, result.Count);

        }
        
        [TestMethod]
        public async Task FindnewTest()
        {

            var findEngle = new SearchEngine(await LoadFindFolderService.LoadDirectoriesAsync(Environment.CurrentDirectory + "\\" + "Test.txt"));

            var findRequest1 = new FindRequest("32446");

            var searchResult = await findEngle.FindAsync(findRequest1, 2);

            Assert.AreEqual(1, searchResult.FindDirectories.Count);

        }
    }
}
