using Microsoft.AspNetCore.Mvc;
using Supermarket_management.Models;

namespace Supermarket_management.Controllers
{
    public class LoginController : Controller
    {
        private readonly SqlsieuThiContext _context;
        public LoginController(SqlsieuThiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.";
                return View();
            }

            var taiKhoan = _context.TaiKhoans
                .FirstOrDefault(t => t.TenDangNhap == tenDangNhap && t.MatKhau == matKhau);

            if (taiKhoan != null)
            {
                // Lưu vào session
                HttpContext.Session.SetString("VaiTro", taiKhoan.VaiTro ?? "");
                HttpContext.Session.SetInt32("MaTaiKhoan", taiKhoan.MaTaiKhoan);

                // Chuyển hướng theo vai trò
                if (taiKhoan.VaiTro == "Admin")
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Index", "Product");
            }
            else
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
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
                HttpContext.Session.SetInt32("MaTaiKhoan", user.MaTaiKhoan);
                HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);
                return RedirectToAction("Index", "Admin");
            }
            else if (user.VaiTro == "User")
            {
                HttpContext.Session.SetString("VaiTro", "User");
                HttpContext.Session.SetInt32("MaTaiKhoan", user.MaTaiKhoan);
                HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);
                return RedirectToAction("Index", "Product");
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

            var existingUser = _context.TaiKhoans
                .FirstOrDefault(x => x.TenDangNhap == model.TenDangNhap);
            if (existingUser != null)
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại.";
                return View(model);
            }

            var newAccount = new TaiKhoan
            {
                TenDangNhap = model.TenDangNhap,
                MatKhau = model.MatKhau,
                VaiTro = model.VaiTro
                // KHÔNG gán MaTaiKhoan
            };
            _context.TaiKhoans.Add(newAccount);
            _context.SaveChanges();
            // newAccount.MaTaiKhoan sẽ tự động nhận giá trị mới từ DB



            // Lấy thông tin khách hàng từ form
            var hoTen = Request.Form["HoTen"];
            var email = Request.Form["Email"];
            var diaChi = Request.Form["DiaChi"];
            var soDienThoai = Request.Form["SoDienThoai"];
            var ngayDangKyStr = Request.Form["NgayDangKy"];
            var ngayDangKy = DateOnly.FromDateTime(DateTime.Now);

            var khachHang = new KhachHang
            {
                // KHÔNG gán MaKhachHang
                HoTen = hoTen,
                Email = email,
                DiaChi = diaChi,
                Sdt = soDienThoai,
                NgayDangKy = ngayDangKy,
                MaKhachHang = newAccount.MaTaiKhoan 
            };
            _context.KhachHangs.Add(khachHang);
            _context.SaveChanges();



            ViewBag.Success = "Tạo tài khoản thành công!";
            return View();
        }
        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear(); // Xóa toàn bộ session
            return RedirectToAction("Index", "Login"); // Quay về trang đăng nhập
        }

    }
}