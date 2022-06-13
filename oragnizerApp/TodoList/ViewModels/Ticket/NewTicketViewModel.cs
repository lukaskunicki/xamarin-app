using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class NewTicketViewModel : BaseDataViewModel<APIClient>
    {
        private Priority _selectedPriority;
        private Employee _assignedEmployee;
        private Employee _reporterEmployee;
        private Sprint _selectedSprint;

        private string title;

        public ObservableCollection<Employee> Employees { get; }
        public ObservableCollection<Priority> Priorities{ get; }
        public ObservableCollection<Sprint> Sprints { get; }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command LoadEmployeesCommand { get; }
        public Command LoadPrioritiesCommand { get; }
        public Command LoadSprintsCommand { get; }


        public NewTicketViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            Employees = new ObservableCollection<Employee>();
            Priorities = new ObservableCollection<Priority>();
            Sprints = new ObservableCollection<Sprint>();

            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());
            LoadPrioritiesCommand = new Command(async () => await ExecuteLoadPrioritiesCommand());
            LoadSprintsCommand = new Command(async () => await ExecuteLoadSprintsCommand());

            ExecuteLoadEmployeesCommand();
            ExecuteLoadPrioritiesCommand();
            ExecuteLoadSprintsCommand();
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
        public Employee AssignedEmployee
        {
            get => _assignedEmployee;
            set
            {
                SetProperty(ref _assignedEmployee, value);
            }
        }
        public Employee ReporterEmployee
        {
            get => _reporterEmployee;
            set
            {
                SetProperty(ref _reporterEmployee, value);
            }
        }
        public Sprint SelectedSprint
        {
            get => _selectedSprint;
            set
            {
                SetProperty(ref _selectedSprint, value);
            }
        }

        public Priority SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                SetProperty(ref _selectedPriority, value);
            }
        }

        async Task ExecuteLoadEmployeesCommand()
        {
            IsBusy = true;

            try
            {
                Employees.Clear();
                var employees = await LoadEmployees();

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

        async Task ExecuteLoadPrioritiesCommand()
        {
            IsBusy = true;

            try
            {
                Priorities.Clear();
                var priorities = await LoadPriorities();

                foreach (var priority in priorities)
                {
                    if (!Priorities.Contains(priority))
                    {
                        Priorities.Add(priority);
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

        async Task ExecuteLoadSprintsCommand()
        {
            IsBusy = true;

            try
            {
                Sprints.Clear();
                var sprints = await LoadSprints();

                foreach (var sprint in sprints)
                {
                    if (!Sprints.Contains(sprint))
                    {
                        Sprints.Add(sprint);
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

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Ticket newTicket = new Ticket()
            {
                Title = title,
                PriorityId = _selectedPriority.PriorityId,
                AssignedEmployeeemployeeId = _assignedEmployee.EmployeeId,
                ReporteremployeeId = _reporterEmployee.EmployeeId,
                SprintId = _selectedSprint.SprintId,
                Created = DateTime.Now,
            };
            try
            {
                await _apiClient.TicketsPOSTAsync(newTicket);
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
