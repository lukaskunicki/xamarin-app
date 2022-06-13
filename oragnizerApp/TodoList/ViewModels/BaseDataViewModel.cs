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

        public async Task<List<Comment>> LoadComments()
        {
            List<Comment> comments = new List<Comment>();
            try
            {
                foreach (var comment in await _apiClient.CommentsAllAsync())
                {
                    comments.Add(comment);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return comments;
        }

        public async Task<List<Team>> LoadTeams()
        {
            List<Team> teams = new List<Team>();
            try
            {
                foreach (var team in await _apiClient.TeamsAllAsync())
                {
                    teams.Add(team);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return teams;
        }

        public async Task<List<Priority>> LoadPriorities()
        {
            List<Priority> priorities = new List<Priority>();
            try
            {
                foreach (var priority in await _apiClient.PrioritiesAllAsync())
                {
                    priorities.Add(priority);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return priorities;
        }
        public async Task<List<Sprint>> LoadSprints()
        {
            List<Sprint> sprints = new List<Sprint>();
            try
            {
                foreach (var sprint in await _apiClient.SprintsAllAsync())
                {
                    sprints.Add(sprint);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return sprints;
        }

        public async Task<List<Project>> LoadProjects()
        {
            List<Project> projects = new List<Project>();
            try
            {
                foreach (var project in await _apiClient.ProjectsAllAsync())
                {
                    projects.Add(project);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return projects;
        }
    }
}
