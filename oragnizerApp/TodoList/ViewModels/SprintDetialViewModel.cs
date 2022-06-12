using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class SprintDetailViewModel : BaseDataViewModel<APIClient>
    {

        private int sprintId;
        private DateTime startTime;
        private DateTime endTime;


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }



        public SprintDetailViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

        }

        private bool ValidateSave()
        {
            return true;
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

            // This will pop the current page off the navigation stack
        }
    }
}
