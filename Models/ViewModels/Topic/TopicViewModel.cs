namespace Beon.Models.ViewModels 
{
  public record TopicViewModel(
    PostViewModel Post,
    int TopicPostId,
    string Title,
    IEnumerable<CommentViewModel> Comments,
    bool CanComment,
    string CannotCommentReason) : PostViewModel(Post)
  {
    //hide
    private PostViewModel Post { get; init; } = default!;

    public static async Task<TopicViewModel> CreateFromAsync(Topic topic, BeonUser? user)
      => new TopicViewModel(
        await PostViewModel.CreateFromAsync(topic, user),
        topic.PostId,
        topic.Title,
        await Task.WhenAll(
          (await topic.GetCommentsAsync()).Select(c => CommentViewModel.CreateFromAsync(c, user))),
        await topic.UserCanCommentAsync(user),
        topic.CannotCommentReason);
  };
}