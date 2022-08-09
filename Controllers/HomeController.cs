using Microsoft.AspNetCore.Mvc;

namespace Beon.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}