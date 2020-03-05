using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;

namespace Bochky.FindDirectory.Core.Services
{
    public class SyncSearchFolderConfigurationService : ISyncSearchFolderConfigurationService
    {

        private readonly ILoadChekedFolderListService _loadChekedFolderListService;

        private readonly IFolderTypeConversionService _folderTypeConversionService;

        private readonly IFindService _findService;

        private readonly string _filePath;

        public SyncSearchFolderConfigurationService(
            ILoadChekedFolderListService loadChekedFolderListService, 
            IFolderTypeConversionService folderTypeConversionService,
            IFindService findService,
            string filePath)
        {
            
            _loadChekedFolderListService = loadChekedFolderListService;
            
            _folderTypeConversionService = folderTypeConversionService;

            _findService = findService;

            _filePath = filePath;

        }


        public async Task<IEnumerable<ChekedFolder>> Pull()
        {

            var localFolderConfiguration = await _loadChekedFolderListService.LoadChekedFolderList(_filePath);

            var searchPoints = await _findService.LoadDirectoriesAsync();

            return await Task.Run(
                () =>_folderTypeConversionService.CompareAndConvertToChekedFolder(
                    localFolderConfiguration, 
                    searchPoints));

        }
    }
}
