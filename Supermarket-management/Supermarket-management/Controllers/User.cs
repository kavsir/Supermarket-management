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

        // Thông tin người dùng (có thể là User hoặc Admin)
        public IActionResult ThongTinCaNhan()
        {
            var vaiTro = HttpContext.Session.GetString("VaiTro");
            var maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");

            if (string.IsNullOrEmpty(vaiTro) || maTaiKhoan == null)
                return RedirectToAction("Index", "Login");

            var taiKhoan = _context.TaiKhoans
                .Include(t => t.KhachHang)
                .Include(t => t.QuanLy)
                .FirstOrDefault(t => t.MaTaiKhoan == maTaiKhoan);

            if (taiKhoan == null)
                return RedirectToAction("Index", "Login");

            if (vaiTro == "User" && taiKhoan.KhachHang != null)
                return View("UserInformation", taiKhoan.KhachHang);

            if (vaiTro == "Admin" && taiKhoan.QuanLy != null)
                return View("ManagerInformation", taiKhoan.QuanLy);

            return RedirectToAction("Index", "Login");
        }
        public IActionResult Search(string query)
        {
            // Kiểm tra nếu chưa đăng nhập thì chuyển về trang đăng nhập
            if (HttpContext.Session.GetString("VaiTro") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // Nếu có đăng nhập, tiếp tục tìm kiếm
            var result = _context.SanPhams
                                 .Where(sp => !string.IsNullOrEmpty(sp.TenSp) && sp.TenSp.Contains(query))
                                 .ToList();

            ViewBag.Keyword = query;
            return View("Search", result);
        }


    }
}
