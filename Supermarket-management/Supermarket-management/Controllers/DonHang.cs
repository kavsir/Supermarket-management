using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket_management.Models;

namespace Supermarket_management.Controllers
{
    public class DonHang : Controller
    {
        private readonly SqlsieuThiContext _context;

        public DonHang(SqlsieuThiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int? maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTaiKhoan == null)
                return RedirectToAction("Index", "Login");

            var khachHang = _context.KhachHangs.FirstOrDefault(k => k.MaKhachHangNavigation.MaTaiKhoan == maTaiKhoan);
            if (khachHang == null)
                return RedirectToAction("Index", "Login");

            var donHangs = _context.DonHangs
                .Where(d => d.MaKhachHang == khachHang.MaKhachHang)
                .Include(d => d.ChiTietDonHangs)
                    .ThenInclude(ct => ct.MaSanPhamNavigation)
                .OrderByDescending(d => d.NgayDat)
                .ToList();

            return View(donHangs);
        }
        public IActionResult HoaDon()
        {
            int? maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTaiKhoan == null)
                return RedirectToAction("Index", "Login");

            var khachHang = _context.KhachHangs.FirstOrDefault(k => k.MaKhachHangNavigation.MaTaiKhoan == maTaiKhoan);
            if (khachHang == null)
                return RedirectToAction("Index", "Login");

            var hoaDons = _context.HoaDons
                .Where(h => h.MaDonHangNavigation != null && h.MaDonHangNavigation.MaKhachHang == khachHang.MaKhachHang)
                .Include(h => h.MaDonHangNavigation)
                    .ThenInclude(dh => dh.ChiTietDonHangs)
                        .ThenInclude(ct => ct.MaSanPhamNavigation)
                .OrderByDescending(h => h.NgayLap)
                .ToList();

            return View(hoaDons);
        }


    }
}
