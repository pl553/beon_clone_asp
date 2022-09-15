using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Beon.Models;
using Beon.Models.ViewModels;
using Beon.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Beon.Components {
  public class CommentViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke(CommentViewModel comment) {
      return View(comment);
    }
  }
}