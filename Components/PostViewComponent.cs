using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Beon.Models;
using Beon.Models.ViewModels;
using Beon.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Beon.Components {
  public class PostViewComponent : ViewComponent
  {
    private readonly IPostRepository _postRepository;
    private readonly UserManager<BeonUser> _userManager;
    private readonly IUserFileRepository _userFileRepository;
    public PostViewComponent(
        IPostRepository postRepository,
        UserManager<BeonUser> userManager,
        IUserFileRepository userFileRepository) {
      _postRepository = postRepository;
      _userManager = userManager;
      _userFileRepository = userFileRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync(int postId, bool showDate = false) {
      Post? post = await _postRepository.Posts
        .Where(p => p.PostId.Equals(postId))
        .FirstOrDefaultAsync();

      if (post == null) {
        throw new Exception("Invalid post id");
      }

      var userInfo = await _userManager.Users
        .Where(u => u.Id.Equals(post.PosterId))
        .Select(u => new { u.UserName, u.DisplayName, u.AvatarFileName })
        .FirstOrDefaultAsync();

      if (userInfo == null) {
        throw new Exception("Invalid post");
      }

      string? avatarUrl = null;
      if (userInfo.AvatarFileName != null) {
        avatarUrl = await _userFileRepository.GetFileUrlAsync(userInfo.UserName, userInfo.AvatarFileName);
      }

      PosterViewModel posterVm = new PosterViewModel(userInfo.UserName, userInfo.DisplayName, avatarUrl);
      PostShowViewModel postVm = new PostShowViewModel(post.Body, post.TimeStamp, posterVm, showDate);
      return View(postVm);
    }
  }
}