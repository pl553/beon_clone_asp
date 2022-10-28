using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using Beon.Models.ViewModels;
using Beon.Models;
using Beon.Services;
using Beon.Infrastructure;

namespace Beon.Controllers
{
  public class PostController : Controller
  {
    private readonly IRepository<Post> _postRepository;
    private readonly UserManager<BeonUser> _userManager;
    private readonly IViewComponentRenderService _vcRender;

    public PostController(
      IRepository<Post> postRepository,
      UserManager<BeonUser> userManager,
      IViewComponentRenderService vcRender)
    {
      _postRepository = postRepository;
      _userManager = userManager;
      _vcRender = vcRender;
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int postId, string returnUrl)
    {
      var post = await _postRepository.Entities
        .Where(p => p.PostId == postId)
        .FirstOrDefaultAsync();

      if (post != null
        && await post.UserCanDeleteAsync(await _userManager.GetUserAsync(User)))
      {
        await _postRepository.DeleteAsync(post);
        return this.RedirectToLocal(returnUrl);
      }
      else
      {
        return NotFound();
      }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetRawHtml(int postId, string? deleteReturnUrl = null)
    {
      var u = await _userManager.GetUserAsync(User);
      var p = await _postRepository.Entities
        .Where(p => p.PostId == postId)
        .FirstOrDefaultAsync();

      if (p == null || !(p is Comment or ChatPost) || !await p.UserCanReadAsync(u))
      {
        return NotFound();
      }

      var vm = await p.CreatePostViewModelAsync(u, deleteReturnUrl);

      string postRawHtml = await _vcRender.RenderAsync(
        ControllerContext,
        ViewData,
        TempData,
        "Post",
        new { post = vm });

      return Content(postRawHtml, "text/html");
    }
  }
}