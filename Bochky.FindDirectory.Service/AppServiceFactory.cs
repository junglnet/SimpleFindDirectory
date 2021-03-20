
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

            var loadFolderFolder = new LoadFolderListFromXML("FindPoint.xml");
            LoadFolderFolder = loadFolderFolder;

            var logger = new NLogLogger("FindDirectory");
            Logger = logger;
        }

        public static AppServiceFactory GetInstance()
        {
            return _current;
        }

        public IFindServiceContract FindServiceContract { get; }

        public ILoadFolderList LoadFolderFolder { get; }

        public ISearchEngine SearchEngine { get; }

        public ILogger Logger { get; }
    }
}
