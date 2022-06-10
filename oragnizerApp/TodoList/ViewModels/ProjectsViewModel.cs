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
    public class ProjectsViewModel : BaseDataViewModel<APIClient>
    {
        private Project _selectedProject;

        public ObservableCollection<Project> Projects { get; }
        public Command LoadProjectsCommand { get; }
        public Command AddProjectCommand { get; }
        public Command<Project> EditProjectCommand { get; }
        public Command<Project> DeleteProjectCommand { get; }
        public Command<Project> ItemTapped { get; }

        public ProjectsViewModel()
        {
            Title = "Projects";
            Projects = new ObservableCollection<Project>();
            LoadProjectsCommand = new Command(async () => await ExecuteLoadProjectsCommand());
            ExecuteLoadProjectsCommand();
            ItemTapped = new Command<Project>(OnProjectselected);
            AddProjectCommand = new Command(OnAddProject);
            EditProjectCommand = new Command<Project>(EditProjectCommandHandler);
            DeleteProjectCommand = new Command<Project>(DeleteProjectCommandHandler);
        }

        async Task ExecuteLoadProjectsCommand()
        {
            IsBusy = true;

            try
            {
                Projects.Clear();
                var projects = await _apiClient.ProjectsAllAsync();

                foreach (var project in projects)
                {
                    if (!Projects.Contains(project))
                    {
                        Projects.Add(project);
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
            SelectedProject = null;
        }

        public Project SelectedProject
        {
            get => _selectedProject;
            set
            {
                SetProperty(ref _selectedProject, value);
                OnProjectselected(value);
            }
        }

        private async void OnAddProject(object obj)
        {

           // await Shell.Current.GoToAsync(nameof(NewProjectPage));
        }

        async void OnProjectselected(Project Project)
        {

            if (Project == null) return;
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void EditProjectCommandHandler(Project Project)
        {
            if (Project == null)
                return;
            // This will push the CategoryDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void DeleteProjectCommandHandler(Project Project)
        {
            try
            {
                await _apiClient.ProjectsDELETEAsync(Project.ProjectId);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error while removing the Project", null, "OK");
            }

            await ExecuteLoadProjectsCommand();
        }
    }
}

