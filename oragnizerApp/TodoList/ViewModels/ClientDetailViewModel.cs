using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    [QueryProperty(nameof(ClientId), nameof(ClientId))]
    public class ClientDetailViewModel : BaseDataViewModel<APIClient>
    {
        private int clientId;
        private string description;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public ClientDetailViewModel()
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

        public int ClientId
        {
            get
            {
                return clientId;
            }
            set
            {
                clientId = value;
                LoadItemId(value);
            }
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var client = await _apiClient.ClientsGETAsync(itemId);

                if (client != null)
                {
                    this.Id = client.ClientId;
                    this.Description = client.Description;
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Serviceman");
            }
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
            };
            var res = await _apiClient.ClientsPOSTAsync(newClient);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
