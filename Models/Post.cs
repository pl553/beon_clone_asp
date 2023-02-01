using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

using Beon.Models.ViewModels;
using Beon.Services;

namespace Beon.Models
{
  public abstract class Post
  {
    public int PostId { get; set; }
    public string Body { get; set; } = String.Empty;
    public DateTime TimeStamp { get; set; }
    public string? PosterId { get; set; }

    public BeonUser? Poster { get; set; }

    [NotMapped]
    [MemberNotNullWhen(false, nameof(PosterId))]
    
    public bool Anonymous { get => PosterId == BeonUser.AnonymousId; }

    private readonly IUserFileRepository _userFileRepository;
    protected readonly BeonDbContext _context;

    protected Post(BeonDbContext context, string body, DateTime timeStamp, string? posterId)
    {
      _userFileRepository = context.GetRequiredService<IUserFileRepository>();
      _context = context;
      Body = body;
      TimeStamp = timeStamp;
      PosterId = posterId;
    }

    public abstract Task<PostViewModel> CreatePostViewModelAsync(BeonUser? user, string? deleteReturnUrl = null);

    public abstract Task<bool> UserCanReadAsync(BeonUser? user);
    
    public bool UserCanEdit(BeonUser? user)
      => user != BeonUser.Anonymous && PosterId == user.Id;

    public virtual Task<bool> UserCanDeleteAsync(BeonUser? user)
      => Task.FromResult(user != BeonUser.Anonymous && PosterId == user.Id);

    public async Task<BeonUser?> GetPosterAsync()
    {
      await _context.Entry(this).Reference(p => p.Poster).LoadAsync();
      return Poster;
    }

    public async Task<BeonUser> GetNonAnonymousPosterAsync()
      => await GetPosterAsync() ?? throw new Exception("this post is anonymous");

    public abstract Task<string> GetEditPathAsync();
  }
}