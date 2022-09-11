using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Beon.Services {
  public class PostLogic {
    private readonly UserManager<BeonUser> _userManager;
    private readonly IRepository<Post> _postRepository;
    private readonly IUserFileRepository _userFileRepository;
    public PostLogic(
      UserManager<BeonUser> userManager,
      IRepository<Post> postRepository,
      IUserFileRepository userFileRepository)
    {
      _postRepository = postRepository;
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
      PostViewModel postVm = new PostViewModel(post.Body, post.TimeStamp, posterVm);
      return postVm;
    }
  }
}