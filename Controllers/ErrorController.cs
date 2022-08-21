using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Beon.Infrastructure;

namespace Beon.Controllers {
  public class ErrorController : Controller {
    [Route("/Error")]
    public IActionResult HandleError() {
      return Problem();
    }
  }
}