using System;
using TodoList.Services;
using TodoList.Services.APIClient;
using TodoList.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoList
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            var apiClient = new APIClient("http://localhost:47215", new System.Net.Http.HttpClient());

            DependencyService.RegisterSingleton<IAPIClient>(apiClient);

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
