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
                HttpContext.Session.SetString("VaiTro", "Admin");
                return RedirectToAction("Index", "Admin");
            }
            else if (user.VaiTro == "User")
            {
                HttpContext.Session.SetString("VaiTro", "User");
                return RedirectToAction("Index", "User");
            }

            // Default return if no role matches
            ViewBag.Error = "Vai trò không hợp lệ.";
            return View();
        }
        public IActionResult CreateAccount()
        {
            var isAdmin = HttpContext.Session.GetString("VaiTro") == "Admin";
            ViewBag.IsAdmin = isAdmin;
            return View();
        }

        //tạo tài khoản
        [HttpPost]
        public IActionResult CreateAccount(TaiKhoan model)
        {
            var isAdmin = HttpContext.Session.GetString("VaiTro") == "Admin";
            ViewBag.IsAdmin = isAdmin;

            if (!isAdmin)
            {
                model.VaiTro = "User";
            }
            // Regex: chỉ cho phép chữ cái không dấu, số, dấu gạch dưới
            var regex = new System.Text.RegularExpressions.Regex("^[A-Za-z0-9_]+$");

            if (string.IsNullOrWhiteSpace(model.TenDangNhap) || string.IsNullOrWhiteSpace(model.MatKhau))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.";
                return View(model);
            }

            if (!regex.IsMatch(model.TenDangNhap) || !regex.IsMatch(model.MatKhau))
            {
                ViewBag.Error = "Tên đăng nhập và mật khẩu chỉ được dùng chữ cái không dấu, số và dấu gạch dưới.";
                return View(model);
            }

            // Kiểm tra trùng tên đăng nhập
            var existingUser = _context.TaiKhoans
                .FirstOrDefault(x => x.TenDangNhap == model.TenDangNhap);
            if (existingUser != null)
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại.";
                return View(model);
            }

            // Không gán MaTaiKhoan, để DB tự động tăng
            var newAccount = new TaiKhoan
            {
                TenDangNhap = model.TenDangNhap,
                MatKhau = model.MatKhau,
                VaiTro = model.VaiTro
            };

            _context.TaiKhoans.Add(newAccount);
            _context.SaveChanges();

            ViewBag.Success = "Tạo tài khoản thành công!";
            return View();
        }

    }
}