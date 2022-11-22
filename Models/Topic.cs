using System.Text;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Beon.Models.ViewModels;
using Beon.Services;
using Microsoft.EntityFrameworkCore;

namespace Beon.Models
{
  public abstract class Topic : Post
  {
    public int TopicOrd { get; set; }
    public string Title { get; set; } = String.Empty;
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public abstract string CannotReadReason { get; }
    public abstract string CannotCommentReason { get; }

    private readonly IRepository<Comment> _commentRepository;
    protected readonly LinkGenerator _linkGenerator;

    protected Topic(
      BeonDbContext context,
      string body,
      DateTime timeStamp,
      string? posterId,
      string title,
      int topicOrd) : base(context, body, timeStamp, posterId)
    {
      _commentRepository = context.Provider.GetRequiredService<IRepository<Comment>>();
      _linkGenerator = context.Provider.GetRequiredService<LinkGenerator>();
      Title = title;
      TopicOrd = topicOrd;
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

    public static string GenerateTitleFromBody(string body)
    {
      var sb = new StringBuilder();

      bool withinSquareBrackets = false;
      bool last = false;
      foreach (var c in body)
      {
        if (sb.Length >= Beon.Settings.Topic.MaxTitleLength - 3 + 1)
        {
          break;
        }
        if (c == '[')
        {
          withinSquareBrackets = true;
        }
        if (c == ']')
        {
          withinSquareBrackets = false;
        }
        if (!withinSquareBrackets && (char.IsLetter(c) || c == ' '))
        {
          if (last)
          {
            sb.Append(' ');
          }
          last = false;
          sb.Append(c);
        }
        else
        {
          last = true;
        }
      }

      if (sb.Length == 0 || sb.Length >= Beon.Settings.Topic.MaxTitleLength - 3 + 1)
      {
        if (sb.Length >= Beon.Settings.Topic.MaxTitleLength - 3 + 1 && sb.Length > 0)
        {
          sb.Remove(sb.Length-1, 1);
        }
        sb.Append("...");
      }
      return sb.ToString();
    }

  }
}