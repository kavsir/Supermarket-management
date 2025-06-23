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

            // Khi tạo mới TaiKhoan, KHÔNG gán MaTaiKhoan (để mặc định là null hoặc 0)
            // Sau khi SaveChanges(), EF sẽ tự động lấy giá trị tự tăng từ DB
            // Lấy mã lớn nhất hiện tại và +1
            int maxMaTaiKhoan = _context.TaiKhoans.Any() ? _context.TaiKhoans.Max(t => t.MaTaiKhoan) : 0;

            var newAccount = new TaiKhoan
            {
                MaTaiKhoan = maxMaTaiKhoan + 1,
                TenDangNhap = model.TenDangNhap,
                MatKhau = model.MatKhau,
                VaiTro = model.VaiTro
            };
            _context.TaiKhoans.Add(newAccount);
            _context.SaveChanges();

            // newAccount.MaTaiKhoan lúc này đã có giá trị mới từ DB


            // Lấy thông tin khách hàng từ form
            var hoTen = Request.Form["HoTen"];
            var email = Request.Form["Email"];
            var diaChi = Request.Form["DiaChi"];
            var soDienThoai = Request.Form["SoDienThoai"];
            var ngayDangKyStr = Request.Form["NgayDangKy"];
            var ngayDangKy = DateOnly.FromDateTime(DateTime.Now);
            // Lấy mã khách hàng lớn nhất hiện tại và +1
            int maxMaKhachHang = _context.KhachHangs.Any() ? _context.KhachHangs.Max(k => k.MaKhachHang) : 0;

            var khachHang = new KhachHang
            {
                MaKhachHang = maxMaKhachHang + 1,
                HoTen = hoTen,
                Email = email,
                DiaChi = diaChi,
                Sdt = soDienThoai,
                NgayDangKy = ngayDangKy,
                MaKhachHangNavigation = newAccount
            };
            _context.KhachHangs.Add(khachHang);
            _context.SaveChanges();


            ViewBag.Success = "Tạo tài khoản thành công!";
            return View();
        }

    }
}