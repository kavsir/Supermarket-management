using Microsoft.AspNetCore.Mvc;
using Supermarket_management.Models;

namespace Supermarket_management.Controllers
{
    public class Login : Controller
    {
        private readonly SqlsieuThiContext _context;
        public Login(SqlsieuThiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(TaiKhoan model)
        {
            var user = _context.TaiKhoans
                .FirstOrDefault(x => x.TenDangNhap == model.TenDangNhap && x.MatKhau == model.MatKhau);

            if (user == null)
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View();
            }
            // Kiểm tra vai trò và chuyển hướng
            if (user.VaiTro == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (user.VaiTro == "User")
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }



    }
}