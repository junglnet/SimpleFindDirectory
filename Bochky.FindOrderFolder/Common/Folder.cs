
namespace Bochky.FindOrderFolder.Common
{
    public class Folder
    {

        public Folder (string directoryName)
        {
            
            DirectoryName = directoryName.ToLower();

            FullDirectoryName = directoryName;

        }

        public string DirectoryName { get; }

        public string FullDirectoryName { get; }

    }
}
