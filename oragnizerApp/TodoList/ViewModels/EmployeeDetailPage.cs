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
    [QueryProperty(nameof(EmployeeId), nameof(EmployeeId))]

    public class EmployeeDetailViewModel : BaseDataViewModel<APIClient>
    {

        private Team _selectedTeam;
        private int employeeId;
        private string name;
        private string surname;


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public ObservableCollection<Team> Teams { get; }
        public Command LoadTeamsCommand { get; }

        public EmployeeDetailViewModel()
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

        public int EmployeeId
        {
            get
            {
                return employeeId;
            }
            set
            {
                employeeId = value;
                LoadItemId(value);
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

        public Team SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                SetProperty(ref _selectedTeam, value);
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var employee = await _apiClient.EmployeesGETAsync(itemId);

                if (employee != null)
                {
                    this.Id = employee.EmployeeId;
                    this.Name = employee.Name;
                    this.Surname = employee.Surname;
                    this.SelectedTeam = employee.Team;
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
            Employee newEmployee = new Employee()
            {
                Name = name,
                Surname = surname,
                TeamId = _selectedTeam.TeamId,
            };
            try
            {
                await _apiClient.EmployeesPUTAsync(newEmployee.EmployeeId, newEmployee);
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
