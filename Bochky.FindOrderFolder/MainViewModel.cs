using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using Bochky.FindDirectory.Entities;
using Bochky.FindDirectory.Implementation;
using Bochky.FindDirectory.Core.Services;
using Bochky.Utils.Logger;

namespace Bochky.FindDirectory
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private string request;
        private string message;
        private bool isDeepSearch;
        private Folder currentFolder;
        private readonly IFindService _findService;
        private IEnumerable<Folder> _searchPoints; 
        private ILogger logger;


        public MainViewModel()
        {
            // defaults
            _findService = new FindService(new FindServiceClient());

            

            IsDeepSearch = false;

            logger = new NLogLogger("FindClient");

            Folders = new ObservableCollection<Folder>();

            SearchPoint = new ObservableCollection<ChekedFolder>();

            LoadCommand = AsyncCommand.Create(async(token) => {

                try
                {

                    ISyncSearchFolderConfigurationService syncSearchFolderConfigurationService
                    = new SyncSearchFolderConfigurationService(
                        new LoadChekedFolderListFromXMLService(),
                        new FolderTypeConversionService(),
                        _findService,
                        "localconfig.xml"
                        );

                    var result = await syncSearchFolderConfigurationService.Pull();

                    foreach (var item in result)
                    {
                        SearchPoint.Add(item);
                    }



                }
                catch (Exception ex)
                {

                    Message = ex.Message;

                    logger.LogError(ex);
                }
            
            
            
            });

            FindCommand = AsyncCommand.Create(async (token) => {

                try
                {

                    await FindAsync(token);
                }

                catch (Exception ex)
                {

                    Message = ex.Message;

                    logger.LogError(ex);
                }

            });

            OpenItemCommand = AsyncCommand.Create(async (token) => {

                try
                {

                    await OpenItem(CurrentFolder, token);

                }

                catch (Exception ex)
                {

                    Message = ex.Message;

                    logger.LogError(ex);
                }

            });
        }

        private async Task FindAsync(CancellationToken token)
        {

                       
            Message = null;

            Folders.Clear();

            var t = new FolderTypeConversionService();

            _searchPoints = t.ConvertToFolder(SearchPoint);

            var searchResult = await _findService.FindAsync(
                     new FindRequest(Request), _searchPoints, IsDeepSearch, token);

            var tg = new SaveChekedFolderListToXMLService();

            await tg.SaveChekedFolderList(SearchPoint, "localconfig.xml");

            if (searchResult.HaveResult)
            {
                Message = "Найдено совпадение.";

                foreach (var item in searchResult.FindDirectories)
                {
                    Folders.Add(item);
                }

            }

            else
                Message = "Ничего не найдено. Попробуйте углубленный поиск.";
        }

        private async Task OpenItem(Folder folder, CancellationToken token) 
            => await DirectoryOpenerService.OpenDirectory(folder, token);            

        public ObservableCollection<Folder> Folders { get; set; }

        public ObservableCollection<ChekedFolder> SearchPoint { get; set; }

        public Folder CurrentFolder
        {
            get => currentFolder;

            set
            {
                currentFolder = value;

                OnPropertyChanged();
            }
        }

        public string Request
        {

            get => request;

            set
            {
                request = value;

                OnPropertyChanged();
            }

        }
        public string Message
        {

            get => message;

            set
            {
                message = value;
                
                OnPropertyChanged();
            }

        }

        public bool IsDeepSearch
        {

            get => isDeepSearch;

            set
            {

                isDeepSearch = value;
                
                OnPropertyChanged();
            }

        }

        public ICommand LoadCommand { get; }
        public ICommand FindCommand { get; }
        
        public ICommand OpenItemCommand { get; }

    }
}
