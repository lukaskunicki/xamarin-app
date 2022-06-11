using System;
using System.Collections.Generic;
using TodoList.ViewModels;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewTeamPage), typeof(NewTeamPage));
            Routing.RegisterRoute(nameof(NewPriorityPage), typeof(NewPriorityPage));
            Routing.RegisterRoute(nameof(NewCommentPage), typeof(NewCommentPage));
            Routing.RegisterRoute(nameof(NewTagPage), typeof(NewTagPage));
            Routing.RegisterRoute(nameof(NewSprintPage), typeof(NewSprintPage));
            Routing.RegisterRoute(nameof(NewEmployeePage), typeof(NewEmployeePage));
            Routing.RegisterRoute(nameof(NewClientPage), typeof(NewClientPage));
            Routing.RegisterRoute(nameof(TagDetailPage), typeof(TagDetailPage));
            Routing.RegisterRoute(nameof(PriorityDetailPage), typeof(PriorityDetailPage));
            Routing.RegisterRoute(nameof(ClientDetailPage), typeof(ClientDetailPage));
            Routing.RegisterRoute(nameof(TeamDetailPage), typeof(TeamDetailPage));

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
