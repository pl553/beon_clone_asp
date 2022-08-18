using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using Beon.Models;
using Beon.Models.ViewModels;

namespace Beon.Controllers
{
  public class HomeController : Controller
  {
    private readonly UserManager<BeonUser> _userManager;
    public HomeController(UserManager<BeonUser> userManager)
    {
      _userManager = userManager;
    }

    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
      ICollection<UserProfileLinkViewModel> users = new List<UserProfileLinkViewModel>();
      var dt = _userManager.Users
        .Select(u => new { u.UserName, u.DisplayName })
        .ToList();

      foreach (var u in dt)
      {
        users.Add(new UserProfileLinkViewModel(u.UserName, u.DisplayName));
      }

      return View(new HomeViewModel(users));
    }
  }
}