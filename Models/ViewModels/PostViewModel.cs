using System.Diagnostics.CodeAnalysis;

namespace Beon.Models.ViewModels {
  public record PostViewModel (
    int PostId,
    string BodyRawHtml,
    DateTime TimeStamp,
    bool CanEdit,
    bool CanDelete,
    PosterViewModel? Poster = null,
    string? DeleteReturnUrl = null
  )

  {
    public PosterViewModel? Poster { get; init; } = Poster;
    
    [MemberNotNullWhen(false, nameof(Poster))]
    public bool Anonymous { get => Poster == null; }
  
    public static async Task<PostViewModel> CreateFromAsync(Post post, BeonUser? user, string? deleteReturnUrl = null)
    => new PostViewModel (
      post.PostId,
      Beon.Infrastructure.BBCode.Parse(post.Body),
      post.TimeStamp,
      post.UserCanEdit(user),
      await post.UserCanDeleteAsync(user),
      await PosterViewModel.CreateFromAsync(await post.GetPosterAsync()),
      deleteReturnUrl
      );
  }
}