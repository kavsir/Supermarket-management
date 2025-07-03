using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket_management.Models; 

namespace Supermarket_management.Controllers
{
    public class User : Controller
    {
        private readonly SqlsieuThiContext _context; 
        public User(SqlsieuThiContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserInformation()
        {
            if (HttpContext.Session.GetString("VaiTro") == null)
                return RedirectToAction("Index", "Login");

            // Lấy mã tài khoản từ session
            int? maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTaiKhoan == null)
                return RedirectToAction("Index", "Login");

            // Lấy thông tin khách hàng từ database
            var khachHang = _context.KhachHangs.FirstOrDefault(k => k.MaKhachHangNavigation.MaTaiKhoan == maTaiKhoan);
            if (khachHang == null)
                return RedirectToAction("Index", "Login");

            return View(khachHang);
        }
    }
}
