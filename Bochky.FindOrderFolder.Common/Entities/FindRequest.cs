using System.Runtime.Serialization;

namespace Bochky.FindDirectory.Common.Entities
{
    [DataContract]
    public class FindRequest
    {

        public FindRequest(string request)
        {
            Request = request;
        }
        
        [DataMember]
        public string Request { get; set; }
    
    }
}
