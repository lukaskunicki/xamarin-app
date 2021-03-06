using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class ClientsViewModel : BaseDataViewModel<APIClient>
    {
        private Client _selectedClient;

        public ObservableCollection<Client> Clients { get; }
        public Command LoadClientsCommand { get; }
        public Command AddClientCommand { get; }
        public Command<Client> EditClientCommand { get; }
        public Command<Client> DeleteClientCommand { get; }
        public Command<Client> ItemTapped { get; }

        public ClientsViewModel()
        {
            Title = "Clients";
            Clients = new ObservableCollection<Client>();
            LoadClientsCommand = new Command(async () => await ExecuteLoadClientsCommand());
            ExecuteLoadClientsCommand();
            ItemTapped = new Command<Client>(OnClientSelected);

            EditClientCommand = new Command<Client>(EditClientCommandHandler);
            DeleteClientCommand = new Command<Client>(DeleteClientCommandHandler);
            AddClientCommand = new Command(OnAddClient);
        }

        async Task ExecuteLoadClientsCommand()
        {
            IsBusy = true;

            try
            {
                Clients.Clear();
                var clients = await LoadClients();

                foreach (var client in clients)
                {
                    if (!Clients.Contains(client))
                    {
                        Clients.Add(client);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void OnAppearing()
        {
            IsBusy = true;
            SelectedClient = null;
        }

        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                SetProperty(ref _selectedClient, value);
                OnClientSelected(value);
            }
        }

        private async void OnAddClient(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewClientPage));
        }

        async void OnClientSelected(Client client)
        {
          
            if (client == null) return;
            await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void EditClientCommandHandler(Client client)
        {
            if (client == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void DeleteClientCommandHandler(Client client)
        {
            try
            {
                await _apiClient.ClientsDELETEAsync(client.ClientId);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("The Client cannot be21 deleted. It may already be used in some item.", null, "OK");
            }

            await ExecuteLoadClientsCommand();
        }
    }
}

