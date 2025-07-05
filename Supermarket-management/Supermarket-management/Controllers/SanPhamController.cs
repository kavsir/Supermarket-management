using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket_management.Models;
using System.IO;
using System.Linq;

public class SanPhamController : Controller
{
    private readonly SqlsieuThiContext _context;
    private readonly IWebHostEnvironment _env;

    public SanPhamController(SqlsieuThiContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public IActionResult Index(string keyword, string MaDanhMuc)
    {
        var query = _context.SanPhams.Include(x => x.MaDanhMucNavigation).AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(sp => sp.TenSp.Contains(keyword));

        if (!string.IsNullOrEmpty(MaDanhMuc))
            query = query.Where(sp => sp.MaDanhMuc == int.Parse(MaDanhMuc));

        ViewBag.DanhMucs = new SelectList(_context.DanhMucs.ToList(), "MaDanhMuc", "TenDanhMuc");
        ViewBag.SelectedMaDanhMuc = MaDanhMuc;
        ViewBag.Keyword = keyword;

        var list = query.OrderBy(sp => sp.MaDanhMuc).ThenBy(sp => sp.MaSp).ToList();
        return View(list);
    }

    public IActionResult Create()
    {
        ViewBag.DanhMucs = new SelectList(_context.DanhMucs.ToList(), "MaDanhMuc", "TenDanhMuc");
        return View();
    }
    [HttpPost]
    public IActionResult Create(SanPham sp, IFormFile upload)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.DanhMucs = new SelectList(_context.DanhMucs.ToList(), "MaDanhMuc", "TenDanhMuc", sp.MaDanhMuc);
            return View(sp);
        }

        if (upload != null && upload.Length > 0)
        {
            var fileName = Path.GetFileName(upload.FileName);
            var filePath = Path.Combine(_env.WebRootPath, "images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                upload.CopyTo(stream);
            }
            sp.HinhAnh = fileName;
        }

        sp.NgayNhap ??= DateOnly.FromDateTime(DateTime.Now);

        _context.SanPhams.Add(sp);
        _context.SaveChanges();

        return RedirectToAction("Index");
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
    public IActionResult Edit(SanPham sp, IFormFile? upload)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.DanhMucs = new SelectList(_context.DanhMucs.ToList(), "MaDanhMuc", "TenDanhMuc", sp.MaDanhMuc);
            return View(sp);
        }

        var spCu = _context.SanPhams.FirstOrDefault(s => s.MaSp == sp.MaSp);
        if (spCu == null) return NotFound();

        // Cập nhật các trường thông tin
        spCu.TenSp = sp.TenSp;
        spCu.MoTa = sp.MoTa;
        spCu.GiaBan = sp.GiaBan;
        spCu.SoLuong = sp.SoLuong;
        spCu.NgayNhap = sp.NgayNhap;
        spCu.TrangThai = sp.TrangThai;
        spCu.MaDanhMuc = sp.MaDanhMuc;

        // Nếu người dùng chọn ảnh mới
        if (upload != null && upload.Length > 0)
        {
            var fileName = Path.GetFileName(upload.FileName);
            var path = Path.Combine(_env.WebRootPath, "images", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);
            }
            spCu.HinhAnh = fileName;
        }

        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var sp = _context.SanPhams.Find(id);
        if (sp == null) return NotFound();

        _context.SanPhams.Remove(sp);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
 
}
