using Microsoft.AspNetCore.Mvc;
using Supermarket_management.Models;

namespace Supermarket_management.Controllers
{
    public class Admin : Controller
    {
        private readonly SqlsieuThiContext _context;

        public Admin(SqlsieuThiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Tạo tài khoản (chỉ admin mới vào được)
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("VaiTro") != "Admin")
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.IsAdmin = true;
            return View();
        }

        public IActionResult CreateAccount()
        {
            if (HttpContext.Session.GetString("VaiTro") != "Admin")
                return RedirectToAction("Index", "Login");

            ViewBag.IsAdmin = true;
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(TaiKhoan model, string HoTen, string Email, string DiaChi, string SoDienThoai)
        {
            if (HttpContext.Session.GetString("VaiTro") != "Admin")
                return RedirectToAction("Index", "Login");

            if (_context.TaiKhoans.Any(x => x.TenDangNhap == model.TenDangNhap))
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại.";
                ViewBag.IsAdmin = true;
                return View(model);
            }

            _context.TaiKhoans.Add(model);
            _context.SaveChanges();

            if (model.VaiTro == "User")
            {
                var kh = new KhachHang
                {
                    MaKhachHang = model.MaTaiKhoan,
                    HoTen = HoTen,
                    Email = Email,
                    DiaChi = DiaChi,
                    Sdt = SoDienThoai,
                    NgayDangKy = DateOnly.FromDateTime(DateTime.Now)
                };
                _context.KhachHangs.Add(kh);
                _context.SaveChanges();
            }
            else if (model.VaiTro == "Admin")
            {
                var ql = new QuanLy
                {
                    MaQuanLy = model.MaTaiKhoan,
                    HoTen = HoTen,
                    Email = Email,
                    Sdt = SoDienThoai
                };
                _context.QuanLies.Add(ql);
                _context.SaveChanges();
            }

            // Gán session tạm để chuyển hướng
            HttpContext.Session.SetString("VaiTro", model.VaiTro ?? "");
            HttpContext.Session.SetInt32("MaTaiKhoan", model.MaTaiKhoan);

            return RedirectToAction("ThongTinCaNhan", "User");
        }
    }
}

