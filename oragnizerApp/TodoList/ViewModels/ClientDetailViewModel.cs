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
            get => Description;
            set => SetProperty(ref description, value);
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var client = await _apiClient.ClientsGETAsync(itemId);
                if (client != null)
                {
                    Id = client.ClientId;
                    Description = client.Description;
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Serviceman");
            }
        }
    }
}
