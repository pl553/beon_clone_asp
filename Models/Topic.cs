using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Beon.Models.ViewModels;
using Beon.Services;
using Microsoft.EntityFrameworkCore;

namespace Beon.Models {
  public abstract class Topic : Post {
    public int TopicOrd { get; set; }
    public string Title { get; set; } = String.Empty;
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public abstract string CannotReadReason { get; }
    public abstract string CannotCommentReason { get; }

    private readonly IRepository<Comment> _commentRepository;
    protected readonly LinkGenerator _linkGenerator;

    protected Topic(BeonDbContext context) : base(context)
    {
      _commentRepository = context.Provider.GetRequiredService<IRepository<Comment>>();
      _linkGenerator = context.Provider.GetRequiredService<LinkGenerator>();
    }

    public abstract Task<TopicPreviewViewModel> CreateTopicPreviewViewModelAsync(
      BeonUser? user);

    public override async Task<PostViewModel> CreatePostViewModelAsync(BeonUser? user, string? deleteReturnUrl = null)
      => await CreateTopicPreviewViewModelAsync(user);
    public async Task<int> GetCommentCountAsync()
      => await _context.Entry(this).Collection(t => t.Comments).Query().CountAsync();

    public async Task<ICollection<Comment>> GetCommentsAsync()
    {
      await _context.Entry(this)
        .Collection(t => t.Comments)
        .Query()
        .Include(c => c.Poster)
        .LoadAsync();
      
      return Comments;
    }

    public abstract Task<string> GetPathAsync();

    public abstract Task<bool> UserCanCommentAsync(BeonUser? user);

    public override async Task<bool> UserCanDeleteAsync(BeonUser? user)
      => UserCanEdit(user) || await UserModeratesAsync(user);

    public abstract Task<bool> UserModeratesAsync(BeonUser? user);
  }
}