using Aciktim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aciktim.Areas.Restaurant.Controllers
{
    [Authorize(Policy = "Restaurant")]
    [Area("Restaurant")]
    public class CategoryController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            ViewBag.id = id;
            List<Category> categories = _context.Categories.Where(c => c.RestaurantId == id).ToList();
            return View(categories);
        }

        public IActionResult Add(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

            return RedirectToAction("Index", new { id = category.RestaurantId });
        }

        public IActionResult Delete(int id)
        {
            List<ProductCategory> productCategories = _context.ProductCategories.Where(x => x.CategoryId == id).ToList();
            foreach (ProductCategory productCategory in productCategories)
            {
                _context.ProductCategories.Remove(productCategory);
            }
            Category c = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (c != null)
            {
                _context.Categories.Remove(c);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = c.RestaurantId });
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (category != null)
            {
                return View("Update", category);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (category != null)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = category.RestaurantId });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
