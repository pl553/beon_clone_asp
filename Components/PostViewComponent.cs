using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Beon.Components {
  public class PostViewComponent : ViewComponent
  {
    private readonly IPostRepository _postRepository;
    private readonly UserManager<BeonUser> _userManager;
    public PostViewComponent(
        IPostRepository postRepository,
        UserManager<BeonUser> userManager) {
      _postRepository = postRepository;
      _userManager = userManager;
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
        .Select(u => new { u.UserName, u.DisplayName })
        .FirstOrDefaultAsync();

      if (userInfo == null) {
        throw new Exception("Invalid post");
      }

      PosterViewModel posterVm = new PosterViewModel(userInfo.UserName, userInfo.DisplayName);
      PostShowViewModel postVm = new PostShowViewModel(post.Body, post.TimeStamp, posterVm, showDate);
      return View(postVm);
    }
  }
}