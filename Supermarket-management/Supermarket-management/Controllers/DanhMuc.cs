using Microsoft.AspNetCore.Mvc;
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
            var danhMucs = _context.DanhMucs.Include(dm => dm.SanPhams).ToList();
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

        public IActionResult Edit(int id)
        {
            var dm = _context.DanhMucs.Find(id);
            if (dm == null) return NotFound();
            return View(dm);
        }

        [HttpPost]
        public IActionResult Edit(DanhMuc dm)
        {
            if (ModelState.IsValid)
            {
                _context.DanhMucs.Update(dm);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dm);
        }

        public IActionResult Delete(int id)
        {
            var dm = _context.DanhMucs.Find(id);
            if (dm == null) return NotFound();

            // Optional: kiểm tra nếu có sản phẩm liên quan thì không xóa
            var hasSanPham = _context.SanPhams.Any(sp => sp.MaDanhMuc == id);
            if (hasSanPham)
            {
                TempData["Error"] = "Không thể xóa danh mục vì có sản phẩm liên quan!";
                return RedirectToAction("Index");
            }

            _context.DanhMucs.Remove(dm);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
