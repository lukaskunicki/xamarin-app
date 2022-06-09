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
    public class SprintsViewModel : BaseDataViewModel<APIClient>
    {
        private Sprint _selectedSprint;

        public ObservableCollection<Sprint> Sprints { get; }
        public Command LoadSprintsCommand { get; }
        public Command AddSprintCommand { get; }
        public Command<Sprint> EditSprintCommand { get; }
        public Command<Sprint> DeleteSprintCommand { get; }
        public Command<Sprint> ItemTapped { get; }

        public SprintsViewModel()
        {
            Title = "Sprints";
            Sprints = new ObservableCollection<Sprint>();
            LoadSprintsCommand = new Command(async () => await ExecuteLoadSprintsCommand());
            ExecuteLoadSprintsCommand();
            ItemTapped = new Command<Sprint>(OnSprintselected);
            AddSprintCommand = new Command(OnAddSprint);
            EditSprintCommand = new Command<Sprint>(EditSprintCommandHandler);
            DeleteSprintCommand = new Command<Sprint>(DeleteSprintCommandHandler);
        }

        async Task ExecuteLoadSprintsCommand()
        {
            IsBusy = true;

            try
            {
                Sprints.Clear();
                var sprints = await _apiClient.SprintsAllAsync();

                foreach (var sprint in sprints)
                {
                    if (!Sprints.Contains(sprint))
                    {
                        Sprints.Add(sprint);
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
            SelectedSprint = null;
        }

        public Sprint SelectedSprint
        {
            get => _selectedSprint;
            set
            {
                SetProperty(ref _selectedSprint, value);
                OnSprintselected(value);
            }
        }

        private async void OnAddSprint(object obj)
        {

            await Shell.Current.GoToAsync(nameof(NewSprintPage));
        }

        async void OnSprintselected(Sprint Sprint)
        {

            if (Sprint == null) return;
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void EditSprintCommandHandler(Sprint Sprint)
        {
            if (Sprint == null)
                return;
            // This will push the CategoryDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void DeleteSprintCommandHandler(Sprint Sprint)
        {
            try
            {
                await _apiClient.SprintsDELETEAsync(Sprint.SprintId);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error while removing the Sprint", null, "OK");
            }

            await ExecuteLoadSprintsCommand();
        }
    }
}

