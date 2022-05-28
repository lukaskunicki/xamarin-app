using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class NewClientViewModel : BaseDataViewModel<APIClient>
    {
        private int id;
        private string name;
        private string address;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewClientViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name);
        }

        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
        /*    AddC newClient = new AddClient()
            {
                Name = Name,
                Address = Address,
            };

            await _apiClient.ClientsPostAsync(newClient);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync(".."); */
        }
    }
}
