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
    public class ProjectDetailViewModel : BaseDataViewModel<APIClient>
    {

        private Employee _selectedEmployee;
        private Client _selectedClient;
        private int projectId;
        private string name;
        private DateTime startTime;
        private DateTime endTime;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public ObservableCollection<Employee> Employees { get; }
        public ObservableCollection<Client> Clients { get; }
        public Command LoadEmployeesCommand { get; }
        public Command LoadClientsCommand { get; }

        public ProjectDetailViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            Employees = new ObservableCollection<Employee>();
            Clients = new ObservableCollection<Client>();
            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());
            LoadClientsCommand = new Command(async () => await ExecuteLoadClientsCommand());
            ExecuteLoadEmployeesCommand();
            ExecuteLoadClientsCommand();
        }

        private bool ValidateSave()
        {
            return true;
        }

        public int Id { get; set; }

        public int ProjectId
        {
            get
            {
                return projectId;
            }
            set
            {
                projectId = value;
            }
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
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

        async Task ExecuteLoadEmployeesCommand()
        {
            IsBusy = true;

            try
            {
                Employees.Clear();
                var employees = await _apiClient.EmployeesAllAsync();

                foreach (var employee in employees)
                {
                    if (!Employees.Contains(employee))
                    {
                        Employees.Add(employee);
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
        async Task ExecuteLoadClientsCommand()
        {
            IsBusy = true;

            try
            {
                Clients.Clear();
                var clients = await _apiClient.ClientsAllAsync();

                foreach (var client in clients)
                {

                    if (!Clients.Contains(client))
                    {
                        Clients.Add(client);
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

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                SetProperty(ref _selectedEmployee, value);
            }
        }
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                SetProperty(ref _selectedClient, value);
            }
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Project newProject = new Project()
            {
                ProjectId = projectId,
                Name = name,
                StartTime = startTime,
                EndTime = endTime,
                projectManageremployeeId = _selectedEmployee.EmployeeId,
                ClientId = _selectedClient.ClientId,

            };
            try
            {
                await _apiClient.ProjectsPUTAsync(newProject.ProjectId, newProject);
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
