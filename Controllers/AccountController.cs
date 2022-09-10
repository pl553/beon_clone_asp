using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Beon.Models;
using Beon.Models.AccountViewModels;
using Beon.Models.ViewModels;
using Beon.Services;
using Beon.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Beon.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<BeonUser> _userManager;
        private readonly SignInManager<BeonUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly IRepository<Diary> _diaryRepository;
        private readonly IRepository<Board> _boardRepository;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<BeonUser> userManager,
            SignInManager<BeonUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            IRepository<Diary> diaryRepository,
            IRepository<Board> boardRepository,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _diaryRepository = diaryRepository;
            _boardRepository = boardRepository;
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
                /*if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }*/
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
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    var diaryBoard = new Board { Name = $"DiaryBoard{user.Id}", Type = BoardType.Diary, OwnerName = model.UserName };
                    await _boardRepository.CreateAsync(diaryBoard);
                    var diary = new Diary { Board = diaryBoard, Owner = user };
                    await _diaryRepository.CreateAsync(diary);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return this.RedirectToLocal("");
        }

        [AllowAnonymous]
        [Route("/users/{userName:required}/")]
        public IActionResult ShowInfo(string userName)
        {
            var info = _userManager.Users
                .Where(u => u.UserName.Equals(userName))
                .Select(u => new { u.DisplayName, u.UserName })
                .FirstOrDefault();
            
            if (info == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.HrBarViewModel = new HrBarViewModel(
                    crumbs: new List<LinkViewModel>
                    {
                        new LinkViewModel("BeOn", "/"),
                        new LinkViewModel("Пользователи", "/users"),
                        new LinkViewModel(info.DisplayName, "")
                    });
                
                return View(new UserProfileViewModel(info.UserName, info.DisplayName));
            }
        }
        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private Task<BeonUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        #endregion
    }
}
