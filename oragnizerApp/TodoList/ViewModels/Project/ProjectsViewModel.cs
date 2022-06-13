using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class ProjectsViewModel : BaseDataViewModel<APIClient>
    {
        public ObservableCollection<Project> Projects { get; }
        public Command LoadProjectsCommand { get; }
        public Command AddProjectCommand { get; }
        public Command<Project> EditProjectCommand { get; }
        public Command<Project> DeleteProjectCommand { get; }

        public ProjectsViewModel()
        {
            Title = "Projects";
            Projects = new ObservableCollection<Project>();
            LoadProjectsCommand = new Command(async () => await ExecuteLoadProjectsCommand());
            ExecuteLoadProjectsCommand();
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
                var projects = await LoadProjects();

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
        }

        private async void OnAddProject(object obj)
        {

            await Shell.Current.GoToAsync(nameof(NewProjectPage));
        }

        async void EditProjectCommandHandler(Project Project)
        {
            if (Project == null) return;
           await Shell.Current.GoToAsync($"{nameof(ProjectDetailPage)}?{nameof(ProjectDetailViewModel.ProjectId)}={Project.ProjectId}");
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

