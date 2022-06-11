using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class TicketsViewModel : BaseDataViewModel<APIClient>
    {
        private Ticket _selectedTicket;

        public ObservableCollection<Ticket> Tickets { get; }
        public Command LoadTicketsCommand { get; }
        public Command AddTicketCommand { get; }
        public Command<Ticket> EditTicketCommand { get; }
        public Command<Ticket> DeleteTicketCommand { get; }
        public Command<Ticket> ItemTapped { get; }

        public TicketsViewModel()
        {
            Title = "Tickets";
            Tickets = new ObservableCollection<Ticket>();
            LoadTicketsCommand = new Command(async () => await ExecuteLoadTicketsCommand());
            ExecuteLoadTicketsCommand();
            ItemTapped = new Command<Ticket>(OnTicketselected);
            AddTicketCommand = new Command(OnAddTicket);
            EditTicketCommand = new Command<Ticket>(EditTicketCommandHandler);
            DeleteTicketCommand = new Command<Ticket>(DeleteTicketCommandHandler);
        }

        async Task ExecuteLoadTicketsCommand()
        {
            IsBusy = true;

            try
            {
                Tickets.Clear();
                var tickets = await _apiClient.TicketsAllAsync();

                foreach (var ticket in tickets)
                {
                    if (!Tickets.Contains(ticket))
                    {
                        Tickets.Add(ticket);
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
            SelectedTicket = null;
        }

        public Ticket SelectedTicket
        {
            get => _selectedTicket;
            set
            {
                SetProperty(ref _selectedTicket, value);
                OnTicketselected(value);
            }
        }

        private async void OnAddTicket(object obj)
        {

            await Shell.Current.GoToAsync(nameof(NewTicketPage));
        }

        async void OnTicketselected(Ticket Ticket)
        {

            if (Ticket == null) return;
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void EditTicketCommandHandler(Ticket Ticket)
        {
            if (Ticket == null)
                return;
            // This will push the CategoryDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(TicketDetailPage)}?{nameof(TicketDetailViewModel.TicketId)}={Ticket.TicketId}");
        }

        async void DeleteTicketCommandHandler(Ticket Ticket)
        {
            try
            {
                await _apiClient.TicketsDELETEAsync(Ticket.TicketId);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error while removing the Ticket", null, "OK");
            }

            await ExecuteLoadTicketsCommand();
        }
    }
}

