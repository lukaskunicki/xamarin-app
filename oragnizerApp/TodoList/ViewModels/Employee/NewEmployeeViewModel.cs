using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class NewEmployeeViewModel : BaseDataViewModel<APIClient>
    {

        private Team _selectedTeam;
        private int employeeId;
        private string name;
        private string surname;


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public ObservableCollection<Team> Teams { get; }
        public Command LoadTeamsCommand { get; }

        public NewEmployeeViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            Teams = new ObservableCollection<Team>();
            LoadTeamsCommand = new Command(async () => await ExecuteLoadTeamsCommand());
            ExecuteLoadTeamsCommand();
        }

        public int EmployeeId
        {
            get
            {
                return employeeId;
            }
            set
            {
                employeeId = value;
            }
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Surname
        {
            get => surname;
            set => SetProperty(ref surname, value);
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

        public Team SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                SetProperty(ref _selectedTeam, value);
            }
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Employee newEmployee = new Employee()
            {
                Name = name,
                Surname = surname,
                TeamId = _selectedTeam.TeamId,
            };

            try
            {
                await _apiClient.EmployeesPOSTAsync(newEmployee);
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
