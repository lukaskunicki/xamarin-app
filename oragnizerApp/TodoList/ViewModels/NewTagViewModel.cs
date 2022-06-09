using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class NewTagViewModel : BaseDataViewModel<APIClient>
    {

        private Tag _selectedTag;
        private int tagId;
        private string description;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public ObservableCollection<Tag> Tags { get; }
        public Command LoadTagsCommand { get; }

        public NewTagViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            Tags = new ObservableCollection<Tag>();
            LoadTagsCommand = new Command(async () => await ExecuteLoadTagsCommand());
            ExecuteLoadTagsCommand();
        }

        private bool ValidateSave()
        {
            return true;
        }

        public int Id { get; set; }

        public int TagId
        {
            get
            {
                return tagId;
            }
            set
            {
                tagId = value;
            }
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        async Task ExecuteLoadTagsCommand()
        {
            IsBusy = true;

            try
            {
                Tags.Clear();
                var tags = await _apiClient.TagsAllAsync();

                foreach (var tag in tags)
                {
                    if (!Tags.Contains(tag))
                    {
                        Tags.Add(tag);
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

        public Tag SelectedTag
        {
            get => _selectedTag;
            set
            {
                SetProperty(ref _selectedTag, value);
            }
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Tag newTag = new Tag()
            {
                TagId = tagId,
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
            // This will pop the current page off the navigation stack
        }
    }
}