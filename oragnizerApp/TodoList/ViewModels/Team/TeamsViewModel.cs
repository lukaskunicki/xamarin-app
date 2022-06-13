using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class TeamsViewModel : BaseDataViewModel<APIClient>
    {
        public ObservableCollection<Team> Teams { get; } 
        public Command LoadTeamsCommand { get; }
        public Command AddTeamCommand { get; }
        public Command<Team> EditTeamCommand { get; }
        public Command<Team> DeleteTeamCommand { get; }

        public TeamsViewModel()
        {
            Title = "Teams";
            Teams = new ObservableCollection<Team>();
            LoadTeamsCommand = new Command(async () => await ExecuteLoadTeamsCommand());
            ExecuteLoadTeamsCommand();
            AddTeamCommand = new Command(OnAddTeam);
            EditTeamCommand = new Command<Team>(EditTeamCommandHandler);
            DeleteTeamCommand = new Command<Team>(DeleteTeamCommandHandler);
        }

        async Task ExecuteLoadTeamsCommand()
        {
            IsBusy = true;

            try
            {
                Teams.Clear();
                var teams = await LoadTeams();

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

        public async void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnAddTeam(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewTeamPage));
        }

        async void EditTeamCommandHandler(Team Team)
        {
            if (Team == null) return;
            await Shell.Current.GoToAsync($"{nameof(TeamDetailPage)}?{nameof(TeamDetailViewModel.TeamId)}={Team.TeamId}");
        }

        async void DeleteTeamCommandHandler(Team Team)
        {
            try
            {
                await _apiClient.TeamsDELETEAsync(Team.TeamId);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error while removing the Team", null, "OK");
            }

            await ExecuteLoadTeamsCommand();
        }
    }
}