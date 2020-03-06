
namespace Bochky.FindDirectory.Common.Interfaces
{
    public interface IServiceFactoryServer
    {
                
        IFindServiceContract FindServiceContract { get; }        
        ILoadFindFolderService LoadFindFolderService { get; }        
        ISearchEngine SearchEngine { get; }
        ILogger Logger { get; }

    }
}
