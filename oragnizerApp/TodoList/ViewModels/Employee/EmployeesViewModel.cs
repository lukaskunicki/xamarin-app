using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class EmployeesViewModel : BaseDataViewModel<APIClient>
    {
        public ObservableCollection<Employee> Employees { get; }
        public Command LoadEmployeesCommand { get; }
        public Command AddEmployeeCommand { get; }
        public Command<Employee> EditEmployeeCommand { get; }
        public Command<Employee> DeleteEmployeeCommand { get; }

        public EmployeesViewModel()
        {
            Title = "Employees";
            Employees = new ObservableCollection<Employee>();
            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());
            ExecuteLoadEmployeesCommand();

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


        public async void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnAddEmployee(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewEmployeePage));
        }

        async void EditEmployeeCommandHandler(Employee employee)
        {
            if (employee == null) return;
            await Shell.Current.GoToAsync($"{nameof(EmployeeDetailPage)}?{nameof(EmployeeDetailViewModel.EmployeeId)}={employee.EmployeeId}");
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

