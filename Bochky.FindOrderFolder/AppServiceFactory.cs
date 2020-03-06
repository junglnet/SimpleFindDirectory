﻿using System;
using Bochky.FindDirectory.Common.Interfaces;
using Bochky.FindDirectory.Core.Services;
using Bochky.FindDirectory.Implementation;
using Bochky.Utils.Logger;

namespace Bochky.FindDirectory
{
    class AppServiceFactory : IServiceFactoryClient
    {


        private static readonly Lazy<AppServiceFactory> _current = new Lazy<AppServiceFactory>(() => new AppServiceFactory());

        private AppServiceFactory()
        {

            var findServiceContract = new FindServiceClient();            
            FindServiceContract = findServiceContract;

            var findService = new FindService(findServiceContract);
            FindService = findService;

            var folderTypeConversionService = new FolderTypeConversionService();
            FolderTypeConversionService = folderTypeConversionService;

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
            Logger = (Common.Interfaces.ILogger)logger;
        }

        public static AppServiceFactory Current
        {
            get => _current.Value;
        }       


        public IFindService FindService { get; }

        public IFindServiceContract FindServiceContract { get; }

        public IFolderTypeConversionService FolderTypeConversionService { get; }

        public ILoadChekedFolderListService LoadChekedFolderListService { get; }

        public ISaveChekedFolderListService SaveChekedFolderListService { get; }

        public ISyncSearchFolderConfigurationService SyncSearchFolderConfigurationService { get; }

        public Common.Interfaces.ILogger Logger { get; }
    }
}