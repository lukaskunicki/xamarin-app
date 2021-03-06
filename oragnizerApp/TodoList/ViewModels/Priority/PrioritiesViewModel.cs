using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class PrioritiesViewModel : BaseDataViewModel<APIClient>
    {
        public ObservableCollection<Priority> Priorities { get; }
        public Command LoadPrioritiesCommand { get; }
        public Command AddPriorityCommand { get; }
        public Command<Priority> EditPriorityCommand { get; }
        public Command<Priority> DeletePriorityCommand { get; }

        public PrioritiesViewModel()
        {
            Title = "Priorities";
            Priorities = new ObservableCollection<Priority>();
            LoadPrioritiesCommand = new Command(async () => await ExecuteLoadPrioritiesCommand());
            ExecuteLoadPrioritiesCommand();
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
                var priorities = await LoadPriorities();

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
        }

        private async void OnAddPriority(object obj)
        {

            await Shell.Current.GoToAsync(nameof(NewPriorityPage));
        }

        async void EditPriorityCommandHandler(Priority Priority)
        {
            if (Priority == null) return;
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

