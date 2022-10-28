using Beon.Models.ViewModels;

namespace Beon.Models
{
  public class ChatPost : Post
  {
    public int ChatId { get; set; }
    public virtual Chat Chat { get; set; } = null!;

    public async override Task<bool> UserCanReadAsync(BeonUser? user)
    {
      throw new NotImplementedException();
    }

    public async override Task<PostViewModel> CreatePostViewModelAsync(BeonUser? user, string? deleteReturnUrl = null)
    {
      throw new NotImplementedException();
    }
  }
}