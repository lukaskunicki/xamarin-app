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
    [QueryProperty(nameof(TeamId), nameof(TeamId))]
    public class TeamDetailViewModel : BaseDataViewModel<APIClient>
    {

        private string teamName;
        private string teamDescription;
        private int teamId;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public ObservableCollection<Team> Teams { get; }
        public Command LoadTeamsCommand { get; }

        public TeamDetailViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            Teams = new ObservableCollection<Team>();
            LoadTeamsCommand = new Command(async () => await ExecuteLoadTeamsCommand());
            ExecuteLoadTeamsCommand();
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
                LoadItemId(value);
            }
        }

        public string TeamDescription
        {
            get => teamDescription;
            set => SetProperty(ref teamDescription, value);
        }
        public string TeamName
        {
            get => teamName;
            set => SetProperty(ref teamName, value);
        }
        async Task ExecuteLoadTeamsCommand()
        {
            IsBusy = true;

            try
            {
                Teams.Clear();
                var teams = await _apiClient.TeamsAllAsync();

                foreach (var team in teams)
                {
                    if (!Teams.Contains(team))
                    {
                        Teams.Add(team);
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
                var team = await _apiClient.TeamsGETAsync(itemId);

                if (team != null)
                {
                    this.Id = team.TeamId;
                    this.TeamDescription = team.TeamDescription;
                    this.TeamName = team.TeamName;
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Serviceman");
            }
        }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Team newTeam = new Team()
            {
                TeamId = TeamId,
                TeamDescription = teamDescription,
                TeamName = teamName,
            };
            try
            {
                await _apiClient.TeamsPUTAsync(newTeam.TeamId, newTeam);
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
