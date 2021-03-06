using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    [QueryProperty(nameof(TicketId), nameof(TicketId))]

    public class TicketDetailViewModel : BaseDataViewModel<APIClient>
    {

        private Employee _selectedEmployee;
        private Priority _selectedPriority;
        private Employee _selectedReporter;
        private Sprint _selectedSprint;
        private int ticketId;
        private string title;
        private DateTime created;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public ObservableCollection<Employee> Employees { get; }
        public ObservableCollection<Employee> Reporters { get; }
        public ObservableCollection<Priority> Priorities { get; }
        public ObservableCollection<Sprint> Sprints { get; }
        public Command LoadEmployeesCommand { get; }
        public Command LoadReportersCommand { get; }
        public Command LoadPrioritiesCommand { get; }
        public Command LoadSprintsCommand { get; }

        public TicketDetailViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();

            Employees = new ObservableCollection<Employee>();
            Reporters = new ObservableCollection<Employee>();
            Priorities = new ObservableCollection<Priority>();
            Sprints = new ObservableCollection<Sprint>();

            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());
            LoadReportersCommand = new Command(async () => await ExecuteLoadReportersCommand());
            LoadPrioritiesCommand = new Command(async () => await ExecuteLoadPrioritiesCommand());
            LoadSprintsCommand = new Command(async () => await ExecuteLoadSprintsCommand());
            ExecuteLoadEmployeesCommand();
            ExecuteLoadReportersCommand();
            ExecuteLoadPrioritiesCommand();
            ExecuteLoadSprintsCommand();
        }

        public int Id { get; set; }

        public int TicketId
        {
            get
            {
                return ticketId;
            }
            set
            {
                ticketId = value;
                LoadItemId(value);
            }
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
        public DateTime Created
        {
            get => created;
            set => SetProperty(ref created, value);
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
        async Task ExecuteLoadReportersCommand()
        {
            IsBusy = true;

            try
            {
                Reporters.Clear();
                var reporters = await LoadEmployees();

                foreach (var reporter in reporters)
                {

                    if (!Reporters.Contains(reporter))
                    {
                        Reporters.Add(reporter);
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

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                SetProperty(ref _selectedEmployee, value);
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
        public Employee SelectedReporter
        {
            get => _selectedReporter;
            set
            {
                SetProperty(ref _selectedReporter, value);
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

        public async void LoadItemId(int itemId)
        {
            try
            {
                var ticket = await _apiClient.TicketsGETAsync(itemId);
                if (ticket != null)
                {
                    this.Id = ticket.TicketId;
                    this.Title = ticket.Title;
                    this.SelectedEmployee = ticket.AssignedEmployee;
                    this.SelectedReporter = ticket.Reporter;
                    this.SelectedPriority = ticket.Priority;
                    this.SelectedSprint = ticket.Sprint;
                    this.Created = ticket.Created;
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Ticket");
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
                TicketId = TicketId,
                Title = title,
                Created = created,
                PriorityId = _selectedEmployee.EmployeeId,
                AssignedEmployeeemployeeId = _selectedEmployee.EmployeeId,
                ReporteremployeeId = _selectedEmployee.EmployeeId,
                SprintId = _selectedEmployee.EmployeeId,
            };
            try
            {
                await _apiClient.TicketsPUTAsync(newTicket.TicketId, newTicket);
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
