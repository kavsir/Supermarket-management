using Microsoft.AspNetCore.Mvc;

namespace Supermarket_management.Controllers
{
    public class User : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
