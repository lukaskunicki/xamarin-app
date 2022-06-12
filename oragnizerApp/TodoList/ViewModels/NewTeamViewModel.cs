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

        private string teamName;
        private string teamDescription;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewTeamViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
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
        }
    }
}
