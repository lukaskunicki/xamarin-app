﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class NewClientViewModel : BaseDataViewModel<APIClient>
    {

        private Employee _selectedEmployee;
        private int clientId;
        private string description;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public ObservableCollection<Employee> Employees { get; }
        public Command LoadEmployeesCommand { get; }

        public NewClientViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            Employees = new ObservableCollection<Employee>();
            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());
            ExecuteLoadEmployeesCommand();
        }

        private bool ValidateSave()
        {
            return true;
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

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                SetProperty(ref _selectedEmployee, value);
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
                await _apiClient.ClientsPOSTAsync(newClient);
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
