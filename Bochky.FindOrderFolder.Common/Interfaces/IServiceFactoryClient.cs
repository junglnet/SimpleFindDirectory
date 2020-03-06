
namespace Bochky.FindDirectory.Common.Interfaces
{
    public interface IServiceFactoryClient
    {
        IFindService FindService { get; }
        IFindServiceContract FindServiceContract { get; }
        IFolderTypeConversionService FolderTypeConversionService { get; }
        ILoadChekedFolderListService LoadChekedFolderListService { get; }
        ISaveChekedFolderListService SaveChekedFolderListService { get; }
        ISyncSearchFolderConfigurationService SyncSearchFolderConfigurationService { get; }
        ILogger Logger { get; }
    }
}
