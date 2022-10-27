using System.Diagnostics.CodeAnalysis;

namespace Beon.Models.ViewModels 
{
  public record TopicPreviewViewModel(
    PostViewModel Post,
    string Path,
    string Title,
    int CommentCount,
    bool CanComment = true
  ) : PostViewModel(Post)
  {
    //hide
    private PostViewModel Post { get; init; } = default!;
    

    [MemberNotNullWhen(returnValue: false, nameof(CannotReadReason))]
    public bool CanRead { get => CannotReadReason == null; }
    
    public string? CannotReadReason { get; set; } = null;
    
    public TopicPreviewViewModel(string cannotReadReason, PosterViewModel? poster) :
     this(
      new PostViewModel(
        PostId: -1, 
        BodyRawHtml: "",
        TimeStamp: default,
        CanEdit: false,
        CanDelete: false,
        poster),
      Path: "",
      Title: "",
      CommentCount: 0,
      CanComment: false)
    {
      CannotReadReason = cannotReadReason;
    }

    public static async Task<TopicPreviewViewModel> CreateFromAsync(Topic topic, BeonUser? user)
    {
      if (!await topic.UserCanReadAsync(user))
      {
        return new TopicPreviewViewModel(
          topic.CannotReadReason,
          await PosterViewModel.CreateFromAsync(await topic.GetPosterAsync()));
      }
      else
      {
        return new TopicPreviewViewModel(
          await PostViewModel.CreateFromAsync(topic, user),
          await topic.GetPathAsync(),
          topic.Title,
          await topic.GetCommentCountAsync(),
          await topic.UserCanCommentAsync(user));
      }
    }
  }
}