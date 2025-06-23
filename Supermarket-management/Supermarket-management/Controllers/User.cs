using Microsoft.AspNetCore.Mvc;

namespace Supermarket_management.Controllers
{
    public class User : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserInformation()
        {
            // Kiểm tra session đăng nhập (ví dụ: VaiTro hoặc tên đăng nhập)
            if (HttpContext.Session.GetString("VaiTro") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            // Nếu đã đăng nhập, hiển thị view
            return View();
        }
    }
}
