using System;
using Bochky.FindDirectory.Common.Interfaces;
using Bochky.FindDirectory.Core.Services;
using Bochky.FindDirectory.Implementation;
using Bochky.Utils.Logger;

namespace Bochky.FindDirectory
{
    sealed class AppServiceFactory : IServiceFactoryClient
    {

        private static readonly AppServiceFactory _current = new AppServiceFactory();

        private AppServiceFactory()
        {

            var findServiceContract = new FindServiceClient();            
            FindServiceContract = findServiceContract;

            var folderTypeConversionService = new FolderTypeConversionService();
            FolderTypeConversionService = folderTypeConversionService;

            var findService = new FindService(
                FindServiceContract, 
                FolderTypeConversionService);
            FindService = findService;
           

            var loadChekedFolderListService = new LoadChekedFolderListFromXMLService();
            LoadChekedFolderListService = loadChekedFolderListService;

            var saveChekedFolderListService = new SaveChekedFolderListToXMLService("localconfig.xml");
            SaveChekedFolderListService = saveChekedFolderListService;

            var syncSearchFolderConfigurationService 
                = new SyncSearchFolderConfigurationService(
                    LoadChekedFolderListService, 
                    FolderTypeConversionService,
                    FindService,
                    "localconfig.xml");
            
            SyncSearchFolderConfigurationService = syncSearchFolderConfigurationService;

            var logger = new NLogLogger("FindClient");
            Logger = logger;
        }

        public static AppServiceFactory GetInstance()
        {
            return _current;
        }


        public IFindService FindService { get; }

        public IFindServiceContract FindServiceContract { get; }

        public IFolderTypeConversionService FolderTypeConversionService { get; }

        public ILoadChekedFolderListService LoadChekedFolderListService { get; }

        public ISaveChekedFolderListService SaveChekedFolderListService { get; }

        public ISyncSearchFolderConfigurationService SyncSearchFolderConfigurationService { get; }

        public ILogger Logger { get; }
    }
}
