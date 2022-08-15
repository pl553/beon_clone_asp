using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Beon.Controllers
{
  public class DiaryController : Controller
  {
    private readonly ILogger _logger;
    private readonly UserManager<BeonUser> _userManager;
    public DiaryController(UserManager<BeonUser> userManager, ILogger<DiaryController> logger) {
      _userManager = userManager;
      _logger = logger;
    }

    [Route("/diary/{userName:required}")]
    public async Task<IActionResult> Show (string userName)
    {
      BeonUser? owner = await _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .Include(u => u.Diary)
        .ThenInclude(d => d.Board)
        .ThenInclude(b => b.Topics)
        .ThenInclude(t => t.Posts)
        .ThenInclude(p => p.Poster)
        .FirstOrDefaultAsync();

      if (owner == null || owner.Diary == null)
      {
        return RedirectToAction("Index", "Board");
      }

      ViewBag.IsDiaryPage = true;
      ViewBag.DiaryTitle = owner.DisplayName;
      ViewBag.DiarySubtitle = owner.DisplayName;
      return View(owner.Diary);
    }
  }
}