using Bochky.FindOrderFolder.Common;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bochky.FindOrderFolder.Logic
{
    public class ResultProcessingService
    {

        public static bool ResultProcessing(SearchResult searchResult)
        {

            if (searchResult == null)
                throw new ArgumentNullException(nameof(searchResult));

            return searchResult.FindDirectories.Count > 0 ? true : false;
                       
        }

        public static async Task OpenDirectory(Folder folder, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;

            if (folder == null)
                throw new ArgumentNullException(nameof(folder));

            await Task.Run(() => Process.Start(
                            "explorer", folder.DirectoryName));
        }
    }
}
