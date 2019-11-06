using Bochky.FindOrderFolder.Common;
using System;

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

        
    }
}
