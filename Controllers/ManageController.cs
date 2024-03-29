using System.Linq;
using System.Threading.Tasks;
using Beon.Models;
using Beon.Models.ViewModels;
using Beon.Models.ManageViewModels;
using Beon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Beons.Controllers
{

    [Authorize]
    public class ManageController : Controller
    {
        private readonly UserManager<BeonUser> _userManager;
        private readonly SignInManager<BeonUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly IUserFileRepository _userFileRepository;
        private readonly LinkGenerator _linkGenerator;

        public ManageController(
        UserManager<BeonUser> userManager,
        SignInManager<BeonUser> signInManager,
        IEmailSender emailSender,
        ISmsSender smsSender,
        ILoggerFactory loggerFactory,
        LinkGenerator linkGenerator,
        IUserFileRepository userFileRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _userFileRepository = userFileRepository;
            _logger = loggerFactory.CreateLogger<ManageController>();
            _linkGenerator = linkGenerator;
        }

        //
        // GET: /Manage/Index
        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.ChangeDisplayNameSuccess ? "Your display name has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var user = await GetCurrentUserAsync();
            var model = new IndexViewModel
            {
                HasPassword = await _userManager.HasPasswordAsync(user),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(user),
                Logins = await _userManager.GetLoginsAsync(user),
                BrowserRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user),
                AuthenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user)
            };

            ViewBag.HrBarViewModel = new HrBarViewModel(crumbs: await GetCrumbsBaseAsync(user));
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangeDisplayName()
        {
            return View();
        }
        //
        // GET: /Manage/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeDisplayName(ChangeDisplayNameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                user.DisplayName = model.DisplayName;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangeDisplayNameSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }
        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        [HttpGet]
        [Authorize]
        [Route("/Manage/Avatars")]
        public async Task<IActionResult> ManageAvatars() {
            var crumbs = await GetCrumbsBaseAsync(await GetCurrentUserAsync());
            crumbs.Add(new LinkViewModel("Загрузка аватар", ""));
            ViewBag.HrBarViewModel = new HrBarViewModel(crumbs: crumbs);
            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("/Manage/Avatars")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageAvatars(AvatarUploadFormModel model) {
            if (!ModelState.IsValid) {
                return View();
            }
            BeonUser? user = await _userManager.GetUserAsync(User);
            if (user == null) {
                return NotFound();
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);

            try {
                await _userFileRepository.SaveFileAsync(user.UserName, fileName, model.File);
            }
            catch (Exception e) {
                ModelState.AddModelError("", "Please try again");
                _logger.LogError(e.ToString());
                return View();
            }

            if (user.AvatarFileName != null) {
                await _userFileRepository.DeleteFileAsync(user.UserName, user.AvatarFileName);
            }

            user.AvatarFileName = fileName;
            await _userManager.UpdateAsync(user);
            
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            ChangeDisplayNameSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        private Task<BeonUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private async Task<ICollection<LinkViewModel>> GetCrumbsBaseAsync(BeonUser user) {
            return new List<LinkViewModel>
            {
                new LinkViewModel("BeOn", "/"),
                new LinkViewModel("Мой дневник", await (await user.GetDiaryAsync()).GetPathAsync()),
                new LinkViewModel("Настройки", _linkGenerator.GetPathByAction("Index", "Manage") ?? "error")
            };
        }
        #endregion
    }
}
