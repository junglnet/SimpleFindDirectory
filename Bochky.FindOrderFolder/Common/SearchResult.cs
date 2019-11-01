using System.Collections.Generic;

namespace Bochky.FindOrderFolder.Common
{
    public class SearchResult
    {

        public SearchResult(IReadOnlyList<Folder> findDirectories, FindRequest findRequest)
        {

            FindDirectories = findDirectories;

            FindRequest = findRequest;

        }


        public IReadOnlyList<Folder> FindDirectories { get; }
        
        public FindRequest FindRequest { get; }

    }
}
