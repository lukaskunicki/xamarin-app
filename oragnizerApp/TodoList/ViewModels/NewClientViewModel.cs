using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class NewClientViewModel : BaseDataViewModel<APIClient>
    {
        private int id;
        private string description;
        private Employee responsibleEmployee;

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
            return true;
        }

        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public string Description
        {
            get => description;
            set => Console.WriteLine(value);
        }

        public Employee ResponsibleEmployee
        {
            get => responsibleEmployee; 
            set => SetProperty(ref responsibleEmployee, value); 
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Client newClient = new Client()
            {
                Description = Description,
                ResponsibleEmployee = ResponsibleEmployee
            };
            Console.WriteLine("fdsfdsfdsfdsfdssd");
            var res = await _apiClient.ClientsPOSTAsync(newClient);
            Console.WriteLine(res);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync(".."); 
        }
    }
}
