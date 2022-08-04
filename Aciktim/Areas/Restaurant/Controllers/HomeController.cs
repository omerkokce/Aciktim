using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aciktim.Areas.Restaurant.Controllers
{
    [Area("Restaurant")]
    public class HomeController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            ViewBag.Products = _context.Products.Where(p => p.RestaurantId == 2).ToList();
            ViewBag.Category = _context.Categories.Where(c => c.RestaurantId == 2).Include(c => c.ProductCategories).ToList();
            return View();
        }
    }
}
