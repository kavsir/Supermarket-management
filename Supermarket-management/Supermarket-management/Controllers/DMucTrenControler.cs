using Microsoft.AspNetCore.Mvc;
using Supermarket_management.Models;

namespace Supermarket_management.Controllers
{
	public class DMucTrenController : BaseController
	{
		public DMucTrenController(SqlsieuThiContext context) : base(context) { }

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Search(string query)
		{
			if (HttpContext.Session.GetString("VaiTro") == null)
				return RedirectToAction("Index", "Login");

			var result = _context.SanPhams
				.Where(sp => sp.TenSp!.Contains(query))
				.ToList();

			return View("Views/User/SearchResult.cshtml", result);
		}
	}
}
