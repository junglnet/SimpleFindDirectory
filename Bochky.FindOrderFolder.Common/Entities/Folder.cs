using System;
using System.Runtime.Serialization;

namespace Bochky.FindDirectory.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Folder
    {
        
        public Folder(string directoryPath)
        {
            DirectoryPath = directoryPath.ToLower();
        }

        public Folder(string directoryPath, string directoryName)
        {
            DirectoryPath = directoryPath.ToLower();

            Name = directoryName;
        }

        [DataMember]
        public string DirectoryPath { get; set; }

        [DataMember]
        public string Name { get; set; }


    }
}
