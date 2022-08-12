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
    public class ProductController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            ViewBag.id = id;
            ViewBag.Products = _context.Products.Where(p => p.RestaurantId == id).ToList();
            ViewBag.Category = PopulateCategories(id);

            return View();
        }

        [HttpPost]
        public IActionResult Add(Product p, string[] category)
        {
            try
            {
                _context.Products.Add(p);
                _context.SaveChanges();
                foreach (string item in category)
                {
                    _context.ProductCategories.Add(new ProductCategory { CategoryId = Convert.ToInt32(item), ProductId = p.ProductId });

                }
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = p.RestaurantId });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Delete(int id)
        {
            List<ProductCategory> productCategories = _context.ProductCategories.Where(x => x.ProductId == id).ToList();
            foreach (ProductCategory productCategory in productCategories)
            {
                _context.ProductCategories.Remove(productCategory);
            }
            Product p = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (p != null)
            {
                _context.Products.Remove(p);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = p.RestaurantId });
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Product product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            List<ProductCategory> pc = _context.ProductCategories.Where(a => a.ProductId == id).ToList();
            List<int> cId = new List<int>();
            ViewBag.Category = PopulateCategories(product.RestaurantId);
            foreach (ProductCategory item in pc)
            {
                cId.Add(item.CategoryId);
            }
            ViewBag.Pc = cId;
            if (product != null)
            {
                return View("Update", product);
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public IActionResult Update(Product p, string[] category)
        {
            if (p != null)
            {
                _context.Products.Update(p);
                _context.SaveChanges();

                foreach (ProductCategory item in _context.ProductCategories.Where(x => x.ProductId == p.ProductId))
                {
                    _context.ProductCategories.Remove(item);
                }
                _context.SaveChanges();

                foreach (string item in category)
                {
                    _context.ProductCategories.Add(new ProductCategory { CategoryId = Convert.ToInt32(item), ProductId = p.ProductId });
                }
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = p.RestaurantId });
            }
            return RedirectToAction("Index");
        }
        private List<SelectListItem> PopulateCategories(int id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using (_context)
            {
                List<Category> categories = _context.Categories.Where(c => c.RestaurantId == id).ToList();
                foreach (Category c in categories)
                    items.Add(new SelectListItem
                    {
                        Text = c.CategoryName.ToString(),
                        Value = c.CategoryId.ToString(),
                    });
            }
            return items;
        }
    }
}