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
    public partial class PriorityDetailPage : ContentPage
    {
        public PriorityDetailPage()
        {
            InitializeComponent();
            BindingContext = new PriorityDetailViewModel();
        }
    }
}