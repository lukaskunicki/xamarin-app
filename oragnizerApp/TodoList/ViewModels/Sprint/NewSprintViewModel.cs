using System;
using System.Diagnostics;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class NewSprintViewModel : BaseDataViewModel<APIClient>
    {

        private DateTime startTime;
        private DateTime endTime;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }



        public NewSprintViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
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


        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Sprint newSprint = new Sprint()
            {
                StartTime = startTime,
                EndTime = endTime,
            };

            try
            {
                await _apiClient.SprintsPOSTAsync(newSprint);
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
