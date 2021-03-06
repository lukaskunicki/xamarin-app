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
        public ObservableCollection<Comment> Comments { get; }
        public Command LoadCommentsCommand { get; }
        public Command AddCommentCommand { get; }
        public Command<Comment> EditCommentCommand { get; }
        public Command<Comment> DeleteCommentCommand { get; }

        public CommentsViewModel()
        {
            Title = "Comments";
            Comments = new ObservableCollection<Comment>();
            LoadCommentsCommand = new Command(async () => await ExecuteLoadCommentsCommand());
            ExecuteLoadCommentsCommand();

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
                var comments = await LoadComments();

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
        }

        private async void OnAddComment(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewCommentPage));
        }

        async void EditCommentCommandHandler(Comment comment)
        {
            if (comment == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(CommentDetailPage)}?{nameof(CommentDetailViewModel.CommentId)}={comment.CommentId}");
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
