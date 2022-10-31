using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Beon.Services;
using Beon.Infrastructure;

namespace Beon.Controllers
{
  public class UserDiaryController : Controller
  {
    private readonly ILogger _logger;
    private readonly UserManager<BeonUser> _userManager;
    private readonly SignInManager<BeonUser> _signInManager;
    private readonly IRepository<UserDiaryEntry> _userDiaryEntryRepository;
    private readonly LinkGenerator _linkGenerator;
    public UserDiaryController(
      UserManager<BeonUser> userManager,
      SignInManager<BeonUser> signInManager,
      IRepository<UserDiaryEntry> userDiaryEntryRepository,
      LinkGenerator linkGenerator,
      ILogger<UserDiaryController> logger)
    {
      _userManager = userManager;
      _linkGenerator = linkGenerator;
      _signInManager = signInManager;
      _userDiaryEntryRepository = userDiaryEntryRepository;
      _logger = logger;
    }

    [Route("/diary/{userName:required}")]
    [Route("/diary/{userName:required}/{page:int}")]
    public async Task<IActionResult> Show(string userName, int page = 1)
    {
      BeonUser? diaryOwner = await _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .Include(u => u.Diary)
        .FirstOrDefaultAsync();
      
      if (diaryOwner == null)
      {
        return NotFound();
      }

      int userDiaryId = (await diaryOwner.GetDiaryAsync()).DiaryId;

      var numPages = await _userDiaryEntryRepository.Entities
        .Where(entry => entry.UserDiaryId == userDiaryId)
        .CountNumberOfPagesAsync();

      if (page > 1 && page > numPages)
      {
        return this.RedirectToLocal(await (await diaryOwner.GetDiaryAsync()).GetPathAsync(numPages));
      }

      var entries = await _userDiaryEntryRepository.Entities
        .Where(entry => entry.UserDiaryId == userDiaryId)
        .TakePage(page)
        .ToListAsync();

      var user = await _userManager.GetUserAsync(User);
      
      ViewBag.IsDiaryPage = true;
      ViewBag.DiaryTitle = diaryOwner.DisplayName;
      ViewBag.DiarySubtitle = diaryOwner.DisplayName;

      ViewBag.HrBarViewModel = new HrBarViewModel(
        crumbs: new List<LinkViewModel> {new LinkViewModel(diaryOwner.DisplayName, "")},
        pagingInfo: new PagingInfo($"/diary/{userName}", page, numPages));

      var previews = new List<UserDiaryEntryPreviewViewModel>();
      foreach (var e in entries)
      {
        previews.Add(await UserDiaryEntryPreviewViewModel.CreateFromAsync(e, user));
      }

      return View(new UserDiaryViewModel(
        userName,
        user == null ? false : diaryOwner.Id == user.Id,
        previews));
    }
  }
}