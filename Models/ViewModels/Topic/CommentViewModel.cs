namespace Beon.Models.ViewModels {
  public record CommentViewModel(
    PostViewModel Post,
    bool CanComment) : PostViewModel(Post)
  {
    //hide
    private PostViewModel Post { get; init; } = null!;

    public static async Task<CommentViewModel> CreateFromAsync(Comment comment, BeonUser? user)
    => new CommentViewModel(
      await PostViewModel.CreateFromAsync(comment, user),
      await (await comment.GetTopicAsync()).UserCanCommentAsync(user));
  }
}