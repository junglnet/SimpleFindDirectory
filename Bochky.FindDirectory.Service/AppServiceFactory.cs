﻿
using Bochky.FindDirectory.Common.Interfaces;
using Bochky.FindDirectory.Service.Implementation;
using Bochky.FindDirectory.Service.Core;
using Bochky.Utils.Logger;

namespace Bochky.FindDirectory.Service
{
    sealed class AppServiceFactory : IServiceFactoryServer
    {

        private static readonly AppServiceFactory _current = new AppServiceFactory();

        private AppServiceFactory()
        {

            var searchEngine = new SearchEngine();
            SearchEngine = searchEngine;

            var findServiceContract = new FindService();
            FindServiceContract = findServiceContract;

            var loadFindFolderService = new LoadFindFolderFromXMLService("FindPoint.xml");
            LoadFindFolderService = loadFindFolderService;

            var logger = new NLogLogger("FindDirectory");
            Logger = logger;
        }

        public static AppServiceFactory GetInstance()
        {
            return _current;
        }


        public IFindServiceContract FindServiceContract { get; }

        public ILoadFindFolderService LoadFindFolderService { get; }

        public ISearchEngine SearchEngine { get; }

        public ILogger Logger { get; }
    }
}
