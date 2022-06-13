using System;
using System.Diagnostics;
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
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

        }

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
        }
    }
}
