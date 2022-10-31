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
        await CreateCommentViewModelsFromAsync(topic, user),
        await topic.UserCanCommentAsync(user),
        topic.CannotCommentReason);

    private static async Task<IEnumerable<CommentViewModel>> CreateCommentViewModelsFromAsync(
      Topic topic, BeonUser? user)
    {
      var comments = await topic.GetCommentsAsync();
      var vms = new List<CommentViewModel>();

      foreach (var c in comments)
      {
        vms.Add(await CommentViewModel.CreateFromAsync(c, user));
      }

      return vms;
    } 
  };
}