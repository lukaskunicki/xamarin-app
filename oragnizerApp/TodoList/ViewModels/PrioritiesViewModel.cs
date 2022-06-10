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
    public class PrioritiesViewModel : BaseDataViewModel<APIClient>
    {
        private Priority _selectedPriority;

        public ObservableCollection<Priority> Priorities { get; }
        public Command LoadPrioritiesCommand { get; }
        public Command AddPriorityCommand { get; }
        public Command<Priority> EditPriorityCommand { get; }
        public Command<Priority> DeletePriorityCommand { get; }
        public Command<Priority> ItemTapped { get; }

        public PrioritiesViewModel()
        {
            Title = "Priorities";
            Priorities = new ObservableCollection<Priority>();
            LoadPrioritiesCommand = new Command(async () => await ExecuteLoadPrioritiesCommand());
            ExecuteLoadPrioritiesCommand();
            ItemTapped = new Command<Priority>(OnPriorityselected);
            AddPriorityCommand = new Command(OnAddPriority);
            EditPriorityCommand = new Command<Priority>(EditPriorityCommandHandler);
            DeletePriorityCommand = new Command<Priority>(DeletePriorityCommandHandler);
        }

        async Task ExecuteLoadPrioritiesCommand()
        {
            IsBusy = true;

            try
            {
                Priorities.Clear();
                var priorities = await _apiClient.PrioritiesAllAsync();

                foreach (var priority in priorities)
                {
                    if (!Priorities.Contains(priority))
                    {
                        Priorities.Add(priority);
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
            SelectedPriority = null;
        }

        public Priority SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                SetProperty(ref _selectedPriority, value);
                OnPriorityselected(value);
            }
        }

        private async void OnAddPriority(object obj)
        {

            await Shell.Current.GoToAsync(nameof(NewPriorityPage));
        }

        async void OnPriorityselected(Priority Priority)
        {

            if (Priority == null) return;
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void EditPriorityCommandHandler(Priority Priority)
        {
            if (Priority == null)
                return;
            // This will push the CategoryDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(PriorityDetailPage)}?{nameof(PriorityDetailViewModel.PriorityId)}={Priority.PriorityId}");
        }

        async void DeletePriorityCommandHandler(Priority Priority)
        {
            try
            {
                await _apiClient.PrioritiesDELETEAsync(Priority.PriorityId);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error while removing the Priority", null, "OK");
            }

            await ExecuteLoadPrioritiesCommand();
        }
    }
}

