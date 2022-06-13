using System;
using System.Diagnostics;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    [QueryProperty(nameof(SprintId), nameof(SprintId))]

    public class SprintDetailViewModel : BaseDataViewModel<APIClient>
    {

        private int sprintId;
        private DateTime startTime;
        private DateTime endTime;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public SprintDetailViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        public int Id { get; set; }

        public int SprintId
        {
            get
            {
                return sprintId;
            }
            set
            {
                sprintId = value;
                LoadItemId(value);
            }
        }

        public DateTime StartTime
        {
            get => startTime;
            set => SetProperty(ref startTime, value);
        }
        public DateTime EndTime
        {
            get => endTime;
            set => SetProperty(ref endTime, value);
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var sprint = await _apiClient.SprintsGETAsync(itemId);

                if (sprint != null)
                {
                    this.Id = sprint.SprintId;
                    this.StartTime = sprint.StartTime;
                    this.EndTime = sprint.EndTime;
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Sprint");
            }
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Sprint newSprint = new Sprint()
            {
                SprintId = sprintId,
                StartTime = startTime,
                EndTime = endTime,
            };
            try
            {
                await _apiClient.SprintsPUTAsync(newSprint.SprintId, newSprint);
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
