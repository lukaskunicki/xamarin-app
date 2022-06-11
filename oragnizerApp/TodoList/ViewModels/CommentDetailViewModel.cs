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
    [QueryProperty(nameof(CommentId), nameof(CommentId))]
    public class CommentDetailViewModel : BaseDataViewModel<APIClient>
    {

        private Employee _selectedEmployee;
        private int commentId;
        private string content;
        private DateTime created;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public ObservableCollection<Employee> Employees { get; }
        public Command LoadEmployeesCommand { get; }

        public CommentDetailViewModel()
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

        public int CommentId
        {
            get
            {
                return commentId;
            }
            set
            {
                commentId = value;
                LoadItemId(value);
            }
        }

        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
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
                var comment = await _apiClient.CommentsGETAsync(itemId);

                if (comment != null)
                {
                    this.Id = comment.CommentId;
                    this.Content = comment.Content;
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Serviceman");
            }
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
            try
            {
                await _apiClient.CommentsPUTAsync(newComment.CommentId, newComment);
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
