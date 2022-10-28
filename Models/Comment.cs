using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Beon.Models.ViewModels;
using Beon.Models;

namespace Beon.Models
{
  public class Comment : Post
  {
    public int TopicPostId { get; set; }
    public Topic? Topic { get; set; }

    public Comment(
      BeonDbContext context,
      string body,
      DateTime timeStamp,
      string? posterId,
      int topicPostId) : base(context, body, timeStamp, posterId)
    {
      TopicPostId = topicPostId;
    }

    public override async Task<PostViewModel> CreatePostViewModelAsync(BeonUser? user, string? deleteReturnUrl = null)
      => await CommentViewModel.CreateFromAsync(this, user, deleteReturnUrl);
    
    public override async Task<bool> UserCanDeleteAsync(BeonUser? user)
      => user != BeonUser.Anonymous
        && (user.Id == PosterId || await (await GetTopicAsync()).UserModeratesAsync(user));

    public async override Task<bool> UserCanReadAsync(BeonUser? user)
      => await (await GetTopicAsync()).UserCanReadAsync(user);

    public async Task<Topic> GetTopicAsync()
    {
      await _context.Entry(this).Reference(c => c.Topic).LoadAsync();
      return Topic ?? throw new Exception("Invalid comment: not attached to a topic");
    }
  }
}