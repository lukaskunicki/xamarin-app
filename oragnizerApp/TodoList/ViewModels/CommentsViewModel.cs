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
    public class CommentsViewModel : BaseDataViewModel<APIClient>
    {
        private Comment _selectedComment;

        public ObservableCollection<Comment> Comments { get; }
        public Command LoadCommentsCommand { get; }
        public Command AddCommentCommand { get; }
        public Command<Comment> EditCommentCommand { get; }
        public Command<Comment> DeleteCommentCommand { get; }
        public Command<Comment> ItemTapped { get; }

        public CommentsViewModel()
        {
            Title = "Comments";
            Comments = new ObservableCollection<Comment>();
            LoadCommentsCommand = new Command(async () => await ExecuteLoadCommentsCommand());
            ExecuteLoadCommentsCommand();
            ItemTapped = new Command<Comment>(OnCommentSelected);

            EditCommentCommand = new Command<Comment>(EditCommentCommandHandler);
            DeleteCommentCommand = new Command<Comment>(DeleteCommentCommandHandler);
            AddCommentCommand = new Command(OnAddComment);
        }

        async Task ExecuteLoadCommentsCommand()
        {
            IsBusy = true;

            try
            {
                Comments.Clear();
                var comments = await _apiClient.CommentsAllAsync();

                foreach (var comment in comments)
                {
                    if (!Comments.Contains(comment))
                    {
                        Comments.Add(comment);
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
            SelectedComment = null;
        }

        public Comment SelectedComment
        {
            get => _selectedComment;
            set
            {
                SetProperty(ref _selectedComment, value);
                OnCommentSelected(value);
            }
        }

        private async void OnAddComment(object obj)
        {
            Debug.WriteLine("fdsfdsfdsfdsfdsfdsfds");
            await Shell.Current.GoToAsync(nameof(NewCommentPage));
        }

        async void OnCommentSelected(Comment comment)
        {

            if (comment == null) return;
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void EditCommentCommandHandler(Comment comment)
        {
            if (comment == null)
                return;
            // This will push the CategoryDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ClientDetailPage)}?{nameof(ClientDetailViewModel.ClientId)}={client.ClientId}");
        }

        async void DeleteCommentCommandHandler(Comment comment)
        {
            try
            {
                await _apiClient.CommentsDELETEAsync(comment.CommentId);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error while removing the comment", null, "OK");
            }

            await ExecuteLoadCommentsCommand();
        }
    }
}
