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
    public class NewTeamViewModel : BaseDataViewModel<APIClient>
    {

        private int teamId;
        private string teamName;
        private string teamDescription;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }



        public NewTeamViewModel()
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

        public int TeamId
        {
            get
            {
                return teamId;
            }
            set
            {
                teamId = value;
            }
        }

        public string TeamName
        {
            get => teamName;
            set => SetProperty(ref teamName, value);
        }
        public string TeamDescription
        {
            get => teamDescription;
            set => SetProperty(ref teamDescription, value);
        }


        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Team newTeam = new Team()
            {
                TeamName = teamName,
                TeamDescription = teamDescription,

            };
            Debug.WriteLine("         ");
            Debug.WriteLine(newTeam.TeamName);
            Debug.WriteLine(newTeam.TeamDescription);



            Debug.WriteLine("         ");

            try
            {
                await _apiClient.TeamsPOSTAsync(newTeam);
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
