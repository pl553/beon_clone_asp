using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Beon.Models;
using Beon.Models.AccountViewModels;
using Beon.Models.ViewModels;
using Beon.Services;
using Beon.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Beon.Controllers
{
  public class AccountController : Controller
  {
    private readonly UserManager<BeonUser> _userManager;
    private readonly SignInManager<BeonUser> _signInManager;
    private readonly IEmailSender _emailSender;
    private readonly ISmsSender _smsSender;
    private readonly IRepository<UserDiary> _userDiaryRepository;
    private readonly ILogger _logger;

    public AccountController(
        UserManager<BeonUser> userManager,
        SignInManager<BeonUser> signInManager,
        IEmailSender emailSender,
        ISmsSender smsSender,
        IRepository<UserDiary> userDiaryRepository,
        ILoggerFactory loggerFactory)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _emailSender = emailSender;
      _smsSender = smsSender;
      _userDiaryRepository = userDiaryRepository;
      _logger = loggerFactory.CreateLogger<AccountController>();
    }

    //
    // GET: /Account/Login
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl = "")
    {
      return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    //
    // POST: /Account/Login
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
      if (ModelState.IsValid)
      {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
          _logger.LogInformation(1, "User logged in.");
          return this.RedirectToLocal(model.ReturnUrl);
        }
        if (result.IsLockedOut)
        {
          _logger.LogWarning(2, "User account locked out.");
          return View("Lockout");
        }
        else
        {
          ModelState.AddModelError(String.Empty, "Invalid login attempt.");
          return View(model);
        }
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }

    //
    // GET: /Account/Register
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register(string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;
      return View();
    }

    //
    // POST: /Account/Register
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;
      if (ModelState.IsValid)
      {
        var user = new BeonUser { UserName = model.UserName, DisplayName = model.UserName };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          var userDiary = new UserDiary { Owner = user };
          await _userDiaryRepository.CreateAsync(userDiary);
          await _signInManager.SignInAsync(user, isPersistent: false);
          _logger.LogInformation(3, "User created a new account with password.");
          return this.RedirectToLocal(returnUrl);
        }
        AddErrors(result);
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }

    //
    // POST: /Account/LogOff
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      _logger.LogInformation(4, "User logged out.");
      return this.RedirectToLocal("");
    }

    [AllowAnonymous]
    [Route("/users/{userName:required}/")]
    public async Task<IActionResult> ShowProfile(string userName)
    {
      var profileUser = await _userManager.GetByUserNameAsync(userName);

      if (profileUser == null)
      {
        return NotFound();
      }
      else
      {
        ViewBag.HrBarViewModel = new HrBarViewModel(
            crumbs: new List<LinkViewModel>
            {
                        new LinkViewModel("BeOn", "/"),
                        new LinkViewModel("????????????????????????", "/users"),
                        new LinkViewModel(profileUser.DisplayName, "")
            });

        var friends = (await profileUser.GetFriendsAsync())
          .Select(u => new UserProfileLinkViewModel(u.UserName, u.DisplayName));

        var friendOf = (await profileUser.GetUsersThatHaveMeInTheirFriendListAsync())
          .Select(u => new UserProfileLinkViewModel(u.UserName, u.DisplayName));

        var mutuals = (await profileUser.GetMutualsAsync())
          .Select(u => new UserProfileLinkViewModel(u.UserName, u.DisplayName));

        BeonUser? user = await GetCurrentUserAsync();
        bool profileUserIsFriend = user == null
          ? false
          : await user.IsFriendsWithAsync(profileUser);

        return View(new UserProfileViewModel(
            profileUser.UserName,
            profileUser.DisplayName,
            friends,
            friendOf,
            mutuals,
            ShowFriendAddingUI: user != null && user.Id != profileUser.Id,
            profileUserIsFriend));
      }
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToFriends(string userName)
    {
      BeonUser? u = await _userManager.GetByUserNameAsync(userName);
      if (u == null)
      {
        return NotFound();
      }
      await (await GetCurrentUserAsync()).SendFriendRequest(u);
      return RedirectToAction(nameof(ShowProfile), new { userName = userName });
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveFromFriends(string userName)
    {
      BeonUser? u = await _userManager.GetByUserNameAsync(userName);
      if (u == null)
      {
        return NotFound();
      }
      await (await GetCurrentUserAsync()).RevokeFriendRequest(u);
      return RedirectToAction(nameof(ShowProfile), new { userName = userName });
    }

    #region Helpers

    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }
    }

    private async Task<BeonUser> GetCurrentUserAsync()
      => await _userManager.GetUserAsync(HttpContext.User);

    #endregion
  }
}
