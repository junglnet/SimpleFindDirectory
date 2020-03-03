using System.Runtime.Serialization;

namespace Bochky.FindDirectory.Common.Entities
{
    [DataContract]
    public class Folder
    {
        
        public Folder(string directoryName)
        {
            DirectoryName = directoryName;
        }

        [DataMember]
        public string DirectoryName { get; set; }
        
    }
}
