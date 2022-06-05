using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class EmployeesViewModel : BaseDataViewModel<APIClient>
    {
        private Employee _selectedEmployee;

        public ObservableCollection<Employee> Employees { get; }
        public Command LoadEmployeesCommand { get; }
        public Command AddEmployeeCommand { get; }
        public Command<Employee> EditEmployeeCommand { get; }
        public Command<Employee> DeleteEmployeeCommand { get; }
        public Command<Employee> ItemTapped { get; }

        public EmployeesViewModel()
        {
            Title = "Employees";
            Employees = new ObservableCollection<Employee>();
            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());
            ExecuteLoadEmployeesCommand();
            ItemTapped = new Command<Employee>(OnEmployeeSelected);

            EditEmployeeCommand = new Command<Employee>(EditEmployeeCommandHandler);
            DeleteEmployeeCommand = new Command<Employee>(DeleteEmployeeCommandHandler);
            AddEmployeeCommand = new Command(OnAddEmployee);
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

        public async void OnAppearing()
        {
            IsBusy = true;
            SelectedEmployee = null;
        }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                SetProperty(ref _selectedEmployee, value);
                OnEmployeeSelected(value);
            }
        }

        private async void OnAddEmployee(object obj)
        {
            //await Shell.Current.GoToAsync(nameof(NewClientPage));
        }

        async void OnEmployeeSelected(Employee employee)
        {

            if (employee == null) return;
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void EditEmployeeCommandHandler(Employee employee)
        {
            if (employee == null)
                return;
            // This will push the CategoryDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void DeleteEmployeeCommandHandler(Employee employee)
        {
            try
            {
                await _apiClient.EmployeesDELETEAsync(employee.EmployeeId) ;
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error while removing the employee", null, "OK");
            }

            await ExecuteLoadEmployeesCommand();
        }
    }
}

