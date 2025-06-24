using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket_management.Models;

namespace Supermarket_management.Controllers
{
    public class GioHangController : Controller
    {
        public IActionResult Index()
        {
            int? maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTaiKhoan == null)
                return RedirectToAction("Index", "Login");

            var khachHang = _context.KhachHangs.FirstOrDefault(k => k.MaKhachHangNavigation.MaTaiKhoan == maTaiKhoan);
            if (khachHang == null)
                return RedirectToAction("Index", "Login");

            var gioHang = _context.GioHangs
                .Include(g => g.ChiTietGioHangs)
                    .ThenInclude(ct => ct.MaSanPhamNavigation)
                .FirstOrDefault(g => g.MaKhachHang == khachHang.MaKhachHang);

            var chiTietGioHang = gioHang?.ChiTietGioHangs.ToList() ?? new List<ChiTietGioHang>();
            return View(chiTietGioHang);
        }
        private readonly SqlsieuThiContext _context;
        public GioHangController(SqlsieuThiContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult AddToCart(int maSp, int soLuong)
        {
            var sanPham = _context.SanPhams.FirstOrDefault(sp => sp.MaSp == maSp);
            if (sanPham == null)
                return NotFound();

            if (soLuong < 1 || sanPham.SoLuong == null || soLuong > sanPham.SoLuong)
            {
                // Không đủ hàng, trả lại view chi tiết với thông báo lỗi
                ViewBag.Error = "Số lượng sản phẩm không đủ. Vui lòng nhập lại!";
                return View("Details", sanPham);
            }

            int? maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTaiKhoan == null)
                return RedirectToAction("Index", "Login");

            var khachHang = _context.KhachHangs.FirstOrDefault(k => k.MaKhachHangNavigation.MaTaiKhoan == maTaiKhoan);
            if (khachHang == null)
                return RedirectToAction("Index", "Login");

            var gioHang = _context.GioHangs.FirstOrDefault(g => g.MaKhachHang == khachHang.MaKhachHang);
            if (gioHang == null)
            {
                gioHang = new Supermarket_management.Models.GioHang
                {
                    MaKhachHang = khachHang.MaKhachHang,
                    NgayTao = DateOnly.FromDateTime(DateTime.Now),
                    TongTien = 0
                };
                _context.GioHangs.Add(gioHang);
                _context.SaveChanges();
            }

            var chiTiet = _context.ChiTietGioHangs.FirstOrDefault(c => c.MaGioHang == gioHang.MaGioHang && c.MaSanPham == maSp);
            if (chiTiet != null)
            {
                if (chiTiet.SoLuong + soLuong > sanPham.SoLuong)
                {
                    ViewBag.Error = "Số lượng sản phẩm không đủ. Vui lòng nhập lại!";
                    return View("Details", sanPham);
                }
                chiTiet.SoLuong += soLuong;
            }
            else
            {
                chiTiet = new ChiTietGioHang
                {
                    MaGioHang = gioHang.MaGioHang,
                    MaSanPham = maSp,
                    SoLuong = soLuong
                };
                _context.ChiTietGioHangs.Add(chiTiet);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "GioHang");
        }


        [HttpPost]
        public IActionResult RemoveFromCart(int maChiTiet)
        {
            var chiTiet = _context.ChiTietGioHangs.FirstOrDefault(c => c.MaChiTiet == maChiTiet);
            if (chiTiet != null)
            {
                _context.ChiTietGioHangs.Remove(chiTiet);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ThanhToan(int maChiTiet)
        {
            // Lấy chi tiết sản phẩm từ giỏ hàng để hiển thị xác nhận
            var chiTiet = _context.ChiTietGioHangs
                .Include(c => c.MaSanPhamNavigation)
                .FirstOrDefault(c => c.MaChiTiet == maChiTiet);
            if (chiTiet == null)
                return RedirectToAction("Index");
            return View(chiTiet);
        }

        [HttpPost]
        public IActionResult DatHang(int maChiTiet)
        {
            int? maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTaiKhoan == null)
                return RedirectToAction("Index", "Login");

            var khachHang = _context.KhachHangs.FirstOrDefault(k => k.MaKhachHangNavigation.MaTaiKhoan == maTaiKhoan);
            if (khachHang == null)
                return RedirectToAction("Index", "Login");

            var chiTiet = _context.ChiTietGioHangs
                .Include(c => c.MaSanPhamNavigation)
                .Include(c => c.MaGioHangNavigation)
                .FirstOrDefault(c =>
                    c.MaChiTiet == maChiTiet &&
                    c.MaGioHangNavigation != null &&
                    c.MaGioHangNavigation.MaKhachHang == khachHang.MaKhachHang);

            if (chiTiet == null)
                return RedirectToAction("Index");

            // Tạo đơn hàng mới
            var donHang = new Supermarket_management.Models.DonHang
            {
                MaKhachHang = khachHang.MaKhachHang,
                NgayDat = DateOnly.FromDateTime(DateTime.Now),
                TrangThai = "Chờ xử lý"
            };
            _context.DonHangs.Add(donHang);
            _context.SaveChanges();

            // Thêm chi tiết đơn hàng
            var chiTietDonHang = new Supermarket_management.Models.ChiTietDonHang
            {
                MaDonHang = donHang.MaDonHang,
                MaSanPham = chiTiet.MaSanPham,
                SoLuong = chiTiet.SoLuong,
                DonGia = chiTiet.MaSanPhamNavigation?.GiaBan ?? 0,
                ThanhTien = (chiTiet.MaSanPhamNavigation?.GiaBan ?? 0) * chiTiet.SoLuong
            };
            _context.ChiTietDonHangs.Add(chiTietDonHang);

            // Xóa sản phẩm này khỏi giỏ hàng
            _context.ChiTietGioHangs.Remove(chiTiet);
            _context.SaveChanges();

            // Tính tổng tiền cho hóa đơn (ở đây chỉ có 1 sản phẩm)
            var tongTien = chiTietDonHang.ThanhTien ?? 0;

            // Tạo hóa đơn mới
            var hoaDon = new HoaDon
            {
                MaDonHang = donHang.MaDonHang,
                NgayLap = DateOnly.FromDateTime(DateTime.Now),
                TongTien = tongTien,
                TrangThaiTt = "Chưa thanh toán"
            };
            _context.HoaDons.Add(hoaDon);
            _context.SaveChanges();

            // Nếu muốn tạo bản ghi thanh toán, thêm đoạn sau (tùy chọn)
            var thanhToan = new ThanhToan
            {
                MaKhachHang = khachHang.MaKhachHang,
                MaHoaDon = hoaDon.MaHoaDon,
                NgayThanhToan = DateOnly.FromDateTime(DateTime.Now),
                SoTien = tongTien,
                PhuongThuc = "Tiền mặt" // hoặc lấy từ form nếu có nhiều phương thức
            };
            _context.ThanhToans.Add(thanhToan);
            _context.SaveChanges();

            return RedirectToAction("Index", "DonHang");
        }


    }
}
