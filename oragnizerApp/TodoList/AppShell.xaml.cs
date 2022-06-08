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
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            //Routing.RegisterRoute(nameof(NewCategoryPage), typeof(NewCategoryPage));
            //Routing.RegisterRoute(nameof(NewServicemanPage), typeof(NewServicemanPage));
            Routing.RegisterRoute(nameof(NewEmployeePage), typeof(NewEmployeePage));
            Routing.RegisterRoute(nameof(ClientDetailPage), typeof(ClientDetailPage));
            Routing.RegisterRoute(nameof(NewClientPage), typeof(NewClientPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
