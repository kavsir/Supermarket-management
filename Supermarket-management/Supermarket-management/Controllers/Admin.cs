using Microsoft.AspNetCore.Mvc;

namespace Supermarket_management.Controllers
{
    public class Admin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
