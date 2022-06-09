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
    public class NewSprintViewModel : BaseDataViewModel<APIClient>
    {

        private int sprintId;
        private DateTime startTime;
        private DateTime endTime;


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }



        public NewSprintViewModel()
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
                StartTime = startTime,
                EndTime = endTime,
              
            };
            Debug.WriteLine("         ");
            Debug.WriteLine(newSprint.StartTime);
            Debug.WriteLine(newSprint.EndTime);
    


            Debug.WriteLine("         ");

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
            // This will pop the current page off the navigation stack
        }
    }
}
