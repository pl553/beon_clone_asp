using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Beon.Services {
  public class PostLogic {
    private readonly UserManager<BeonUser> _userManager;
    private readonly IRepository<Topic> _topicRepository;
    private readonly IUserFileRepository _userFileRepository;
    public PostLogic(
      UserManager<BeonUser> userManager,
      IRepository<Topic> topicRepository,
      IUserFileRepository userFileRepository)
    {
      _topicRepository = topicRepository;
      _userManager = userManager;
      _userFileRepository = userFileRepository;
    }
    
    public async Task<PostViewModel> GetPostViewModelAsync(Post post) {
      BeonUser? poster = post.Poster;
      if (post.Poster == null) {
        poster = await _userManager.Users
          .Where(u => u.Id.Equals(post.PosterId))
          .FirstOrDefaultAsync();
      }
      if (poster == null) {
        throw new Exception("invalid post");
      }
      string? avatarUrl = null;
      if (poster.AvatarFileName != null) {
        avatarUrl = await _userFileRepository.GetFileUrlAsync(poster.UserName, poster.AvatarFileName);
      }

      PosterViewModel posterVm = new PosterViewModel(poster.UserName, poster.DisplayName, avatarUrl);
      PostViewModel postVm = new PostViewModel(
        Beon.Infrastructure.BBCode.Parse(post.Body),
        post.TimeStamp,
        posterVm);
      return postVm;
    }

    public async Task<CommentViewModel> GetCommentViewModelAsync(Comment post, BeonUser? user)
    {
      var pvm = await GetPostViewModelAsync(post);

      bool canDelete = false;
      if (user != null)
      {
        canDelete = await UserMayDeleteCommentAsync(post, user);
      }
      return new CommentViewModel(pvm, canDelete);
    }
    public async Task<bool> UserMayDeleteCommentAsync(Comment post, BeonUser user)
    {
      if (post.PosterId.Equals(user.Id))
      {
        return true;
      }
      Topic? t = post.Topic;
      if (t == null)
      {
        t = await _topicRepository.Entities
          .Where(t => t.TopicId.Equals(post.TopicId))
          .FirstOrDefaultAsync();
      }
      if (t == null)
      {
        throw new Exception("invalid post: not attached to a topic");
      }
      return (t.PosterId == null ? false : t.PosterId.Equals(user.Id));
    }
  }
}