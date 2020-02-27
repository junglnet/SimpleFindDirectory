using System.Collections.Generic;

namespace Bochky.FindOrderFolder.Common.Entities
{
    public class SearchResult
    {

        public SearchResult(
            IReadOnlyList<Folder> findDirectories, 
            FindRequest findRequest, 
            bool haveResult)
        {

            FindDirectories = findDirectories;

            FindRequest = findRequest;

            HaveResult = haveResult;
        }


        public IReadOnlyList<Folder> FindDirectories { get; }        
        public FindRequest FindRequest { get; }       
        public bool HaveResult { get; }
    }
}
