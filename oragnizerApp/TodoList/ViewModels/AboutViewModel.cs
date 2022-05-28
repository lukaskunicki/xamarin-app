using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.ViewModels
{
    internal class AboutViewModel : BaseViewModel
    {
        public List<string> authors { get; set; }

        public AboutViewModel()
        {
            authors = new List<string> {"Łukasz Kunicki", "Michał Janik", "Mateusz Burnagiel"};
            Title = "About";
        }
    }
}