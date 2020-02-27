
namespace Bochky.FindOrderFolder.Common.Entities
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
