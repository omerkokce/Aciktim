using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aciktim.Areas.Restaurant.Controllers
{
    [Area("Restaurant")]
    public class MenuController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            List<Menu> menus = _context.Menus.Where(m => m.RestaurantId == 2).ToList();
            ViewBag.Product = _context.Products.Where(x => x.RestaurantId == 2).ToList();
            return View(menus);
        }
        public IActionResult Delete(int id)
        {
            List<ProductMenu> pm = _context.ProductMenus.Where(x => x.MenuId == id).ToList();
            foreach (ProductMenu item in pm)
            {
                _context.ProductMenus.Remove(item);
            }

            Menu menu = _context.Menus.FirstOrDefault(m => m.MenuId == id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Menu menu = _context.Menus.FirstOrDefault(x => x.MenuId == id);
            if (menu != null)
            {
                return View("Update", menu);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(Menu menu)
        {
            if (menu != null)
            {
                _context.Menus.Update(menu);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
