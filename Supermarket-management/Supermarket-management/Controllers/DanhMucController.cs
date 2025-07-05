using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket_management.Models;
using System.Linq;

namespace Supermarket_management.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly SqlsieuThiContext _context;

        public DanhMucController(SqlsieuThiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var danhMucs = _context.DanhMucs
                                   .Include(dm => dm.SanPhams)
                                   .ToList();
            return View(danhMucs);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(DanhMuc dm)
        {
            if (ModelState.IsValid)
            {
                _context.DanhMucs.Add(dm);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var sp = _context.SanPhams.FirstOrDefault(s => s.MaSp == id);
            if (sp == null) return NotFound();

            ViewBag.DanhMucs = new SelectList(_context.DanhMucs.ToList(), "MaDanhMuc", "TenDanhMuc", sp.MaDanhMuc);
            return View(sp);
        }

        [HttpPost]
        public IActionResult Edit(int id, int MaDanhMuc)
        {
            var sp = _context.SanPhams.FirstOrDefault(s => s.MaSp == id);
            if (sp == null) return NotFound();

            sp.MaDanhMuc = MaDanhMuc;
            _context.SaveChanges();

            return RedirectToAction("Index", "DanhMuc");
        }


        // Gỡ sản phẩm khỏi danh mục (không xóa sản phẩm)
        public IActionResult RemoveFromCategory(int id)
        {
            var sp = _context.SanPhams.FirstOrDefault(s => s.MaSp == id);
            if (sp == null) return NotFound();

            sp.MaDanhMuc = null;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Xóa danh mục nếu không có sản phẩm liên quan
        public IActionResult Delete(int id)
        {
            var dm = _context.DanhMucs.Find(id);
            if (dm == null) return NotFound();

            var hasSanPham = _context.SanPhams.Any(sp => sp.MaDanhMuc == id);
            if (hasSanPham)
            {
                TempData["Error"] = "Không thể xóa danh mục vì còn sản phẩm liên quan!";
                return RedirectToAction("Index");
            }

            _context.DanhMucs.Remove(dm);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
