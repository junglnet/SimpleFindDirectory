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
        private readonly IServiceFactoryClient _serviceFactoryClient;
        private string request;
        private string message;
        private bool isDeepSearch;
        private Folder currentFolder;               
       


        public MainViewModel()
        {
            // defaults

            _serviceFactoryClient = AppServiceFactory.Current;

            IsDeepSearch = false;

           

            Folders = new ObservableCollection<Folder>();

            SearchPoint = new ObservableCollection<ChekedFolder>();

            LoadCommand = AsyncCommand.Create(async(token) => {

                try
                {

                    var result
                    = await _serviceFactoryClient.SyncSearchFolderConfigurationService.Pull();
                    

                    foreach (var item in result)
                    {
                        SearchPoint.Add(item);
                    }

                }
                catch (Exception ex)
                {

                    Message = ex.Message;

                    _serviceFactoryClient.Logger.LogError(ex);
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

                    _serviceFactoryClient.Logger.LogError(ex);
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

                    _serviceFactoryClient.Logger.LogError(ex);
                }

            });
        }

        private async Task FindAsync(CancellationToken token)
        {

                       
            Message = null;

            Folders.Clear();

            var searchResult = 
                await _serviceFactoryClient.FindService.FindAsync(
                     Request,
                     SearchPoint, 
                     IsDeepSearch, token);
                      

            await _serviceFactoryClient
                .SaveChekedFolderListService
                .SaveChekedFolderList(SearchPoint);

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
