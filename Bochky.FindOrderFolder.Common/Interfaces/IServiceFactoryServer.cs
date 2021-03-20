
namespace Bochky.FindDirectory.Common.Interfaces
{
    public interface IServiceFactoryServer
    {
                
        IFindServiceContract FindServiceContract { get; }
        ILoadFolderList LoadFolderFolder { get; }        
        ISearchEngine SearchEngine { get; }
        ILogger Logger { get; }

    }
}
