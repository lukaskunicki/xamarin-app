using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class SprintsViewModel : BaseDataViewModel<APIClient>
    {
        public ObservableCollection<Sprint> Sprints { get; }
        public Command LoadSprintsCommand { get; }
        public Command AddSprintCommand { get; }
        public Command<Sprint> EditSprintCommand { get; }
        public Command<Sprint> DeleteSprintCommand { get; }

        public SprintsViewModel()
        {
            Title = "Sprints";
            Sprints = new ObservableCollection<Sprint>();
            LoadSprintsCommand = new Command(async () => await ExecuteLoadSprintsCommand());
            ExecuteLoadSprintsCommand();
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
                var sprints = await LoadSprints();

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
        }

        private async void OnAddSprint(object obj)
        {

            await Shell.Current.GoToAsync(nameof(NewSprintPage));
        }

        async void EditSprintCommandHandler(Sprint Sprint)
        {
            if (Sprint == null) return;
            await Shell.Current.GoToAsync($"{nameof(SprintDetailPage)}?{nameof(SprintDetailViewModel.SprintId)}={Sprint.SprintId}");
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

