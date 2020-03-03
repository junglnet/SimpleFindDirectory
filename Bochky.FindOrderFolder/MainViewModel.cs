﻿using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Entities;
using Bochky.FindDirectory.Logic;
using Bochky.FindDirectory.Implementation;
using Bochky.FindDirectory.Interfaces;
using Bochky.FindDirectory.Services;

namespace Bochky.FindDirectory
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private string request;
        private string message;
        private bool isDeepSearch;
        private Folder currentFolder;
        private readonly IFindService _findService;

        public MainViewModel()
        {
            // defaults
            _findService = new FindService(new FindServiceClient());

            IsDeepSearch = false;

            Folders = new ObservableCollection<Folder>();

            FindCommand = AsyncCommand.Create(async (token) => {

                try
                {

                    await FindAsync(token);
                }

                catch (Exception ex)
                {

                    Message = ex.Message;

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
                }

            });
        }

        private async Task FindAsync(CancellationToken token)
        {
            Message = null;

            Folders.Clear();

            var searchResult = await _findService.FindAsync(
                     new FindRequest() { Request = Request ?? "", }, IsDeepSearch, token);

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
        {
            
            if (folder == null) return;

            await DirectoryOpener.OpenDirectory(folder, token);            

        }

        public ObservableCollection<Folder> Folders { get; set; }

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


        public ICommand FindCommand { get; }
        
        public ICommand OpenItemCommand { get; }

    }
}
