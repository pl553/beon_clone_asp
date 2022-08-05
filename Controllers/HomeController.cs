using Microsoft.AspNetCore.Mvc;
namespace Beon.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index() => View();

    public IActionResult Privacy() => View();
  }
}