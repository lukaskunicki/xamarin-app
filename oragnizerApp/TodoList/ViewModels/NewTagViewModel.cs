using System;
using System.Diagnostics;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class NewTagViewModel : BaseDataViewModel<APIClient>
    {

        private string description;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewTagViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Tag newTag = new Tag()
            {
                Description = description,
            };
            try
            {
                await _apiClient.TagsPOSTAsync(newTag);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}