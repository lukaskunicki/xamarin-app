using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsPage : ContentPage
    {
        ReportsViewModel _viewModel;

        public ReportsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ReportsViewModel();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}