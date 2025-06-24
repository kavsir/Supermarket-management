using Microsoft.AspNetCore.Mvc;
using Supermarket_management.Models;

namespace Supermarket_management.Controllers
{
    public class Product : Controller
    {
        private readonly SqlsieuThiContext _context;
        public Product(SqlsieuThiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.SanPhams.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = _context.SanPhams.FirstOrDefault(p => p.MaSp == id);
            if (product == null)
                return NotFound();
            return View(product);
        }
    }
}