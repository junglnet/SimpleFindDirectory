
namespace Bochky.FindOrderFolder.Common
{
    public class Folder
    {

        public Folder (string directoryName)
        {
            
            DirectoryName = directoryName.ToLower();

        }

        public string DirectoryName { get; }

    }
}
