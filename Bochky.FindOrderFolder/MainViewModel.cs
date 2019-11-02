using System;
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

        public MainViewModel()
        {

            ItetationLevel = 1;

            FindCommand = AsyncCommand.Create(async (token) => {

                try
                {

                    Message = null;

                    var findEngle = new SearchEngine(
                        await LoadFindFolderService.LoadDirectoriesAsync(
                            Environment.CurrentDirectory + "\\" + "FindFolder.cfg"));

                    var searchResult 
                        = await findEngle.FindAsync(
                            new FindRequest(Request ?? ""), ItetationLevel, token);

                    if (await ResultProcessingService.ResultProcessing(searchResult))                    
                        Message = "Найдено совпадение.";                        
                                            
                    else
                        Message = "Ничего не найдено. Попробуйте увеличить глубину.";

                }

                catch (Exception ex)
                {

                    Message = ex.Message;
                }

               

            });
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
        
        public ICommand CancelCommand { get; }

    }
}
