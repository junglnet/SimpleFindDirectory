
using System;

namespace Bochky.FindDirectory.Common.Entities
{

    [Serializable]
    public class ChekedFolder : IEquatable<ChekedFolder>
    {

        public ChekedFolder()
        {

        }

        public ChekedFolder (Folder folder, bool isCheked)
        {
            
            Folder = folder;

            IsCheked = isCheked;

        }
        public Folder Folder { get; set; }

        public bool IsCheked { get; set; }

        public bool Equals(ChekedFolder other)
        {
            if (other is null)
                return false;

            return Folder.DirectoryPath == other.Folder.DirectoryPath;
        }

        public override bool Equals(object obj) => Equals(obj as ChekedFolder);
        public override int GetHashCode() => (Folder.DirectoryPath).GetHashCode();
    }
}
