using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    [QueryProperty(nameof(ClientId), nameof(ClientId))]
    public class ClientDetailViewModel : BaseDataViewModel<APIClient>
    {

        private Employee _selectedEmployee;
        private int clientId;
        private string description;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public ObservableCollection<Employee> Employees { get; }
        public Command LoadEmployeesCommand { get; }

        public ClientDetailViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            Employees = new ObservableCollection<Employee>();
            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());
            ExecuteLoadEmployeesCommand();
        }

        public int Id { get; set; }

        public int ClientId
        {
            get
            {
                return clientId;
            }
            set
            {
                clientId = value;
                LoadItemId(value);
            }
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
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

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                SetProperty(ref _selectedEmployee, value);
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var client = await _apiClient.ClientsGETAsync(itemId);

                if (client != null)
                {
                    this.Id = client.ClientId;
                    this.Description = client.Description;
                    this.SelectedEmployee = client.ResponsibleEmployee;
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Client");
            }
        }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Client newClient = new Client()
            {
                ClientId = clientId,
                Description = description,
                ResponsibleEmployeeemployeeId = _selectedEmployee.EmployeeId,
            };
            try
            {
                await _apiClient.ClientsPUTAsync(newClient.ClientId, newClient);
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
