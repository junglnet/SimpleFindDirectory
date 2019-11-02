using Bochky.FindOrderFolder.Common;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bochky.FindOrderFolder.Logic
{
    public class ResultProcessingService
    {

        public static async Task<bool> ResultProcessing(SearchResult searchResult)
        {

            if (searchResult == null)
                throw new ArgumentNullException(nameof(searchResult));

            if (searchResult.FindDirectories.Count > 0)
            {
                await Task.Run(() => Process.Start(
                            "explorer", searchResult.FindDirectories.First().DirectoryName));

                return true;
            }

            else
                return false;

            
        }

    }
}
