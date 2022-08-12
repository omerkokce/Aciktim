using Aciktim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Aciktim.Areas.Restaurant.Controllers
{
    [Authorize(Policy = "Restaurant")]
    [Area("Restaurant")]
    public class MenuController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            ViewBag.id = id;
            List<Menu> menus = _context.Menus.Where(m => m.RestaurantId == id).ToList();
            
            ViewBag.Product = PopulateProduct(id);
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
                _context.SaveChanges();
                return RedirectToAction("Index", new { menu.RestaurantId });
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Menu menu = _context.Menus.FirstOrDefault(x => x.MenuId == id);
            List<ProductMenu> pm = _context.ProductMenus.Where(a => a.MenuId == id).ToList();
            List<int> pId = new List<int>();
            ViewBag.Product = PopulateProduct(menu.RestaurantId);
            foreach (ProductMenu item in pm)
            {
                pId.Add(item.ProductId);
            }
            ViewBag.Pm = pId;
            if (menu != null)
            {
                return View("Update", menu);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Update(Menu menu, string[] product)
        {
            if (menu != null)
            {
                _context.Menus.Update(menu);
                _context.SaveChanges();

                foreach (ProductMenu item in _context.ProductMenus.Where(x => x.MenuId == menu.MenuId))
                {
                    _context.ProductMenus.Remove(item);
                }
                _context.SaveChanges();

                foreach (string item in product)
                {
                    _context.ProductMenus.Add(new ProductMenu { ProductId = Convert.ToInt32(item), MenuId = menu.MenuId });
                }
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = menu.RestaurantId });
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Add(Menu menu, string[] product)
        {
            try
            {
                _context.Menus.Add(menu);
                _context.SaveChanges();
                foreach (string item in product)
                {
                    _context.ProductMenus.Add(new ProductMenu { ProductId = Convert.ToInt32(item), MenuId = menu.MenuId });
                }
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = menu.RestaurantId });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private List<SelectListItem> PopulateProduct(int id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using (_context)
            {
                List<Product> products = _context.Products.Where(c => c.RestaurantId == id).ToList();
                foreach (Product p in products)
                    items.Add(new SelectListItem
                    {
                        Text = p.ProductName.ToString(),
                        Value = p.ProductId.ToString(),
                    });
            }
            return items;
        }
    }
}
