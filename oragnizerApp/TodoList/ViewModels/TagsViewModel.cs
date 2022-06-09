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
    public class TagsViewModel : BaseDataViewModel<APIClient>
    {
        private Tag _selectedTag;

        public ObservableCollection<Tag> Tags { get; }
        public Command LoadTagsCommand { get; }
        public Command AddTagsCommand { get; }
        public Command<Tag> EditTagCommand { get; }
        public Command<Tag> DeleteTagCommand { get; }
        public Command<Tag> ItemTapped { get; }

        public TagsViewModel()
        {
            Title = "Tags";
            Tags = new ObservableCollection<Tag>();
            LoadTagsCommand = new Command(async () => await ExecuteLoadTagsCommand());
            ExecuteLoadTagsCommand();
            ItemTapped = new Command<Tag>(OnTagselected);

            EditTagCommand = new Command<Tag>(EditTagCommandHandler);
            DeleteTagCommand = new Command<Tag>(DeleteTagCommandHandler);
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

        public async void OnAppearing()
        {
            IsBusy = true;
            SelectedTag = null;
        }

        public Tag SelectedTag
        {
            get => _selectedTag;
            set
            {
                SetProperty(ref _selectedTag, value);
                OnTagselected(value);
            }
        }

        private async void OnAddTag(object obj)
        {
            Debug.WriteLine("fdsfdsfdsfdsfdsfdsfds");
          //  await Shell.Current.GoToAsync(nameof(NewTagPage));
        }

        async void OnTagselected(Tag Tag)
        {

            if (Tag == null) return;
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void EditTagCommandHandler(Tag Tag)
        {
            if (Tag == null)
                return;
            // This will push the CategoryDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void DeleteTagCommandHandler(Tag Tag)
        {
            try
            {
                await _apiClient.TagsDELETEAsync(Tag.TagId);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error while removing the Tag", null, "OK");
            }

            await ExecuteLoadTagsCommand();
        }
    }
}