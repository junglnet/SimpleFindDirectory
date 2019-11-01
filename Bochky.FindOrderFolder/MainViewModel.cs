using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Bochky.FindOrderFolder.Common;
using Bochky.FindOrderFolder.Logic;

namespace Bochky.FindOrderFolder
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private string request;
        private string errorMessage;
        private int maxIteration;

        public MainViewModel()
        {

            MaxIteration = 1;

            FindCommand = AsyncCommand.Create(async (token) => {

                try
                {
                    var findEngle = new FindEngle(
                        await LoadFindFolderService.LoadDirectoriesAsync(
                            Environment.CurrentDirectory + "\\" + "Test.txt"));

                    var t = await findEngle.FindAsync(new FindRequest(Request), MaxIteration);

                    ErrorMessage = t.FindDirectories.Count.ToString();

                }

                catch (Exception ex)
                {

                    ErrorMessage = ex.Message;
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
        public string ErrorMessage
        {

            get => errorMessage;

            set
            {
                errorMessage = value;

                OnPropertyChanged();
            }

        }

        public int MaxIteration
        {

            get => maxIteration;

            set
            {
               
                maxIteration = value;

                OnPropertyChanged();
            }

        }


        public ICommand FindCommand { get; }
        
        public ICommand CancelCommand { get; }

    }
}
