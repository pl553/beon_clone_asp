using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Beon.Models;
using Beon.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Beon.Infrastructure
{
  public static class ControllerExtensions {
    public static IActionResult RedirectToLocal(this Controller controller, string url) {
      if (controller.Url.IsLocalUrl(url))
      {
        return controller.Redirect(url);
      }
      else
      {
        return controller.RedirectToAction(nameof(HomeController.Index), "Home");
      }
    }
  }
}