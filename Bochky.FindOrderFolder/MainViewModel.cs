using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Bochky.FindOrderFolder.Common;
using Bochky.FindOrderFolder.Logic;

namespace Bochky.FindOrderFolder
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private string request;
        private string message;
        private int itetationLevel;
        private Folder currentFolder;

        public MainViewModel()
        {
            // defaults
            ItetationLevel = 1;

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

            var findEngle = new SearchEngine(
                await LoadFindFolderService.LoadDirectoriesAsync(
                    Environment.CurrentDirectory + "\\" + "FindFolder.cfg"));

            var searchResult
                = await findEngle.FindAsync(
                    new FindRequest(Request ?? ""), ItetationLevel, token);

            
            if (ResultProcessingService.ResultProcessing(searchResult))
            {
                Message = "Найдено совпадение.";

                foreach(var item in searchResult.FindDirectories)
                {
                    Folders.Add(item);
                }

            }
                

            else
                Message = "Ничего не найдено. Попробуйте увеличить глубину.";
        }

        private async Task OpenItem(Folder folder, CancellationToken token)
        {
            
            if (folder == null) return;

            await ResultProcessingService.OpenDirectory(folder, token);            

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

        public int ItetationLevel
        {

            get => itetationLevel;

            set
            {

                itetationLevel = value;
                
                OnPropertyChanged();
            }

        }


        public ICommand FindCommand { get; }
        
        public ICommand OpenItemCommand { get; }

    }
}
