using System;
using System.Runtime.Serialization;

namespace Bochky.FindDirectory.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Folder : IEquatable<Folder>
    {

        public Folder() { }

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

        public bool Equals(Folder other)
        {
            if (other is null)
                return false;                      

            return DirectoryPath == other.DirectoryPath;
        }

        public override bool Equals(object obj) => Equals(obj as Folder);
        public override int GetHashCode() => (DirectoryPath).GetHashCode();
    }
}
