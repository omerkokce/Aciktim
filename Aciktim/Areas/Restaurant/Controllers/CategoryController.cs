using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Areas.Restaurant.Controllers
{
    [Area("Restaurant")]
    public class CategoryController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            List<Category> categories =  _context.Categories.Where(c => c.RestaurantId == 2).ToList();
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

            return RedirectToAction("Index");
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
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (category != null)
            {
                return View("Update", category);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (category != null)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
