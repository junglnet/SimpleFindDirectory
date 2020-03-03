using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bochky.FindDirectory.Common.Entities
{
    [DataContract]
    public class SearchResult
    {

        public SearchResult(
            FindRequest findRequest, 
            IEnumerable<Folder> findDirectories, 
            bool haveResult)
        {

            FindRequest = FindRequest;

            FindDirectories = findDirectories;

            HaveResult = haveResult;
        }

        [DataMember]
        public IEnumerable<Folder> FindDirectories { get; set; }
        
        [DataMember]
        public FindRequest FindRequest { get; set; }
        
        [DataMember]
        public bool HaveResult { get; set; }
    }
}
