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
    public class NewCommentViewModel : BaseDataViewModel<APIClient>
    {

        private Employee _selectedEmployee;
        private int commentId;
        private string content;
        private DateTime created;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public ObservableCollection<Employee> Employees { get; }
        public Command LoadCommentCommand { get; }

        public NewCommentViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            Employees = new ObservableCollection<Employee>();
            LoadCommentCommand = new Command(async () => await ExecuteLoadEmployeesCommand());
            ExecuteLoadEmployeesCommand();
        }

        private bool ValidateSave()
        {
            return true;
        }

        public int Id { get; set; }

        public int CommentId
        {
            get
            {
                return commentId;
            }
            set
            {
                commentId = value;
            }
        }

        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
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

        public DateTime Created
        {
            get => created;
            set => SetProperty(ref created, value);
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Comment newComment = new Comment()
            {
                CommentId = commentId,
                Content = content,
                AssignedEmployeeemployeeId = _selectedEmployee.EmployeeId,
            };


            Debug.WriteLine("---------------------------");
            Debug.WriteLine(newComment.CommentId);
            Debug.WriteLine(newComment.Content);
            Debug.WriteLine(newComment.AssignedEmployeeemployeeId);

            Debug.WriteLine("-----------------------");
            try
            {
                await _apiClient.CommentsPOSTAsync(newComment);
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
