using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    [QueryProperty(nameof(PriorityId), nameof(PriorityId))]
    public class PriorityDetailViewModel : BaseDataViewModel<APIClient>
    {

        private string name;
        private int priorityId;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public ObservableCollection<Priority> Priorities { get; }
        public Command LoadPrioritiesCommand { get; }

        public PriorityDetailViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();

            Priorities = new ObservableCollection<Priority>();
            LoadPrioritiesCommand = new Command(async () => await ExecuteLoadPrioritiesCommand());
            ExecuteLoadPrioritiesCommand();
        }
        public int Id { get; set; }

        public int PriorityId
        {
            get
            {
                return priorityId;
            }
            set
            {
                priorityId = value;
                LoadItemId(value);
            }
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
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

        public async void LoadItemId(int itemId)
        {
            try
            {
                var priority = await _apiClient.PrioritiesGETAsync(itemId);

                if (priority != null)
                {
                    Id = priority.PriorityId;
                    Name = priority.Name;
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Priority");
            }
        }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Priority newPriority = new Priority()
            {
                PriorityId = PriorityId,
                Name = name,
            };
            try
            {
                await _apiClient.PrioritiesPUTAsync(newPriority.PriorityId, newPriority);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                await Shell.Current.GoToAsync("..");
            }

        }
    }
}
