using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services.APIClient;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class TagsViewModel : BaseDataViewModel<APIClient>
    {
        public ObservableCollection<Tag> Tags { get; }
        public Command LoadTagsCommand { get; }
        public Command AddTagCommand { get; }
        public Command<Tag> EditTagCommand { get; }
        public Command<Tag> DeleteTagCommand { get; }
        public Command<Tag> ItemTapped { get; }

        public TagsViewModel()
        {
            Title = "Tags";
            Tags = new ObservableCollection<Tag>();
            LoadTagsCommand = new Command(async () => await ExecuteLoadTagsCommand());
            ExecuteLoadTagsCommand();
            AddTagCommand = new Command<Tag>(OnAddTag);
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
        }

        private async void OnAddTag(object obj)
        {
           
         await Shell.Current.GoToAsync(nameof(NewTagPage));
        }

        async void EditTagCommandHandler(Tag Tag)
        {
            if (Tag == null) return;
            await Shell.Current.GoToAsync($"{nameof(TagDetailPage)}?{nameof(TagDetailViewModel.TagId)}={Tag.TagId}");
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