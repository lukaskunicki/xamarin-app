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
    public class TeamsViewModel : BaseDataViewModel<APIClient>
    {
        private Team _selectedTeam;

        public ObservableCollection<Team> Teams { get; }
        public Command LoadTeamsCommand { get; }
        public Command AddTeamsCommand { get; }
        public Command<Team> EditTeamCommand { get; }
        public Command<Team> DeleteTeamCommand { get; }
        public Command<Team> ItemTapped { get; }

        public TeamsViewModel()
        {
            Title = "Teams";
            Teams = new ObservableCollection<Team>();
            LoadTeamsCommand = new Command(async () => await ExecuteLoadTeamsCommand());
            ExecuteLoadTeamsCommand();
            ItemTapped = new Command<Team>(OnTeamselected);

            EditTeamCommand = new Command<Team>(EditTeamCommandHandler);
            DeleteTeamCommand = new Command<Team>(DeleteTeamCommandHandler);
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

        public async void OnAppearing()
        {
            IsBusy = true;
            SelectedTeam = null;
        }

        public Team SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                SetProperty(ref _selectedTeam, value);
                OnTeamselected(value);
            }
        }

        private async void OnAddTeam(object obj)
        {
            //await Shell.Current.GoToAsync(nameof(NewClientPage));
        }

        async void OnTeamselected(Team Team)
        {

            if (Team == null) return;
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void EditTeamCommandHandler(Team Team)
        {
            if (Team == null)
                return;
            // This will push the CategoryDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
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

