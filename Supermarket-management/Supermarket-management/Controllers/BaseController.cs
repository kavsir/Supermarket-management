using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Supermarket_management.Models;

namespace Supermarket_management.Controllers
{
    public class BaseController : Controller
    {
        protected readonly SqlsieuThiContext _context;

        public BaseController(SqlsieuThiContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Lấy danh mục cho dropdown nếu đã đăng nhập
            if (HttpContext.Session.GetString("VaiTro") != null)
            {
                ViewBag.DanhMucs = _context.DanhMucs.ToList();
            }

            base.OnActionExecuting(context);
        }
    }
}
