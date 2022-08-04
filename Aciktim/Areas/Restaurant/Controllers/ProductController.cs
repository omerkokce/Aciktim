using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Aciktim.Areas.Restaurant.Controllers
{
    [Area("Restaurant")]
    public class ProductController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            ViewBag.Products = _context.Products.Where(p => p.RestaurantId == 2).ToList();
            ViewBag.Category = PopulateCategories();

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
            }
            catch (Exception e)
            {

                throw e;
            }

            return RedirectToAction("Index");
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
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private List<SelectListItem> PopulateCategories()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using (_context)
            {
                List<Category> categories = _context.GetCategoryName(2).ToList();
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