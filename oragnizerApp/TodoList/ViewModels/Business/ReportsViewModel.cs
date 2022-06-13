using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using TodoList.Services.Business;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class ReportsViewModel : BaseDataViewModel<APIClient>
    {
        private Employee _selectedEmployee;
        private Sprint _selectedSprint;

        public ObservableCollection<Report> Reports { get; }
        public ObservableCollection<Ticket> Tickets { get; }
        public ObservableCollection<Employee> Employees { get; }
        public ObservableCollection<Sprint> Sprints { get; }


        public Command LoadReportsCommand { get; }

        public Command LoadEmployeesCommand { get; }


        public Command<Report> ShowReportCommand { get; }

        public ReportsViewModel()
        {
            Title = "Reports";
            Reports = new ObservableCollection<Report>();
            Tickets = new ObservableCollection<Ticket>();
            Employees = new ObservableCollection<Employee>();
            Sprints = new ObservableCollection<Sprint>();

            LoadReportsCommand = new Command(async () => await ExecuteLoadReportsCommand());
            ExecuteLoadReportsCommand();
            ExecuteLoadEmployeesCommand();
            ExecuteLoadSprintsCommand();
            ShowReportCommand = new Command<Report>(ShowReportCommandHandler);
            IsBusy = false;
        }

        async Task ExecuteLoadReportsCommand()
        {
            IsBusy = true;

            try
            {
                Reports.Clear();
                var reports = new ObservableCollection<Report>()
                {
                    new Report("Tickets assigned to employee", "getTicketsAssignedToEmployee"),
                    new Report("Tickets reported by employee", "getTicketsReportedByEmployee"),
                    new Report("Tickets from sprint", "getTicketsFromSprint"),
                };

                foreach (var report in reports)
                {
                    if (!Reports.Contains(report))
                    {
                        Reports.Add(report);
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

        async Task ExecuteLoadTicketsCommand(string action, int id)
        {
            IsBusy = true;

            try
            {
                Tickets.Clear();
                var tickets = await _apiClient.ReportsAsync(id, action);

                foreach (var ticket in tickets)
                {
                    if (!Tickets.Contains(ticket))
                    {
                        Tickets.Add(ticket);
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

        public Sprint SelectedSprint
        {
            get => _selectedSprint;
            set
            {
                SetProperty(ref _selectedSprint, value);
            }
        }


        public async void OnAppearing()
        {
            IsBusy = true;
        }

        async void ShowReportCommandHandler(Report report)
        {
            if (report == null) return;
            int targetId = 0;

            if (report.Action == "getTicketsFromSprint")
            {
                if (SelectedSprint == null) return;
                targetId = SelectedSprint.SprintId;
            }
            if (report.Action == "getTicketsReportedByEmployee")
            {
                if (SelectedEmployee == null) return;
                targetId = SelectedEmployee.EmployeeId;
            }
            if (report.Action == "getTicketsAssignedToEmployee")
            {
                if (SelectedEmployee == null) return;
                targetId = SelectedEmployee.EmployeeId;
            }

            if (targetId == 0) return;
            await ExecuteLoadTicketsCommand(report.Action, targetId);
        }
    }
}

