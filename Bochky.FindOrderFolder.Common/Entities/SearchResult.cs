using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bochky.FindDirectory.Common.Entities
{
    public class SearchResult
    {
               
            
        public IEnumerable<Folder> FindDirectories { get; set; }
    
        public FindRequest FindRequest { get; set; }
    
        public bool HaveResult { get; set; }
    }
}
