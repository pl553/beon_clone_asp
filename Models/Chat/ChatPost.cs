using Beon.Models.ViewModels;

namespace Beon.Models
{
  public class ChatPost : Post
  {
    public int ChatId { get; set; }
    public Chat? Chat { get; set; }

    public ChatPost(
      BeonDbContext context,
      string body,
      DateTime timeStamp,
      string? posterId,
      int chatId) : base(context, body, timeStamp, posterId)
    {
      ChatId = chatId;
    }

    public override async Task<string> GetEditPathAsync()
    {
      return "";
    }


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