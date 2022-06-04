using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using Xamarin.Forms;
using Xamarin.Forms.MultiSelectListView;

namespace TodoList.ViewModels
{
    public class BaseDataViewModel<T> : BaseViewModel
    {
        protected IAPIClient _apiClient => DependencyService.Get<IAPIClient>();

        public async Task<List<Client>> LoadClients()
        {
            List<Client> clients = new List<Client>();
            try
            {
                foreach (var client in await _apiClient.ClientsAllAsync())
                {
                    Debug.WriteLine(client);
                    clients.Add(client);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return clients;
        }

        public async Task<List<Employee>> LoadEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                foreach (var employee in await _apiClient.EmployeesAllAsync())
                {
                    employees.Add(employee);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return employees;
        }
    }
}
