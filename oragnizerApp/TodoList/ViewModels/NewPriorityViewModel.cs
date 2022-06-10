using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class NewPriorityViewModel : BaseDataViewModel<APIClient>
    {

        private int priorityId;
        private string name;


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }



        public NewPriorityViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

        }

        private bool ValidateSave()
        {
            return true;
        }

        public int Id { get; set; }

        public int PriorityId
        {
            get
            {
                return priorityId;
            }
            set
            {
                priorityId = value;
            }
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }


        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Priority newPriority = new Priority()
            {
                Name = name,

            };
            Debug.WriteLine("         ");
            Debug.WriteLine(newPriority.Name);



            Debug.WriteLine("         ");

            try
            {
                await _apiClient.PrioritiesPOSTAsync(newPriority);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                await Shell.Current.GoToAsync("..");
            }
            // This will pop the current page off the navigation stack
        }
    }
}
