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
            ViewBag.Products = _context.Products.Include(x => x.Image).Where(p => p.RestaurantId == id).ToList();
            ViewBag.Category = PopulateCategories(id);

            return View();
        }

        [HttpPost]
        public IActionResult Add(Product p, string[] category, AddImage image)
        {
            Image i = new Image();
            if (image.ImageUrl != null)
            {
                string name = image.ImageUrl.FileName.Split(".")[0];
                i.FileName = name;

                var extension = Path.GetExtension(image.ImageUrl.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                image.ImageUrl.CopyTo(stream);
                i.ImageUrl = newImageName;

                _context.Images.Add(i);
                _context.SaveChanges();
                p.ImageId = i.ImageId;
            }
            else
            {
                p.ImageId = 1;
            }

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
            //Delete Category of Product
            List<ProductCategory> productCategories = _context.ProductCategories.Where(x => x.ProductId == id).ToList();
            foreach (ProductCategory productCategory in productCategories)
            {
                _context.ProductCategories.Remove(productCategory);
            }

            //Delete Product from menus
            List<ProductMenu> productMenus = _context.ProductMenus.Where(x => x.ProductId == id).ToList();
            foreach (ProductMenu item in productMenus)
            {
                _context.ProductMenus.Remove(item);
            }

            //Delete Product from oreders
            List<OrderProduct> orderProducts = _context.OrderProducts.Where(x => x.ProductId == id).ToList();
            foreach (OrderProduct item in orderProducts)
            {
                _context.OrderProducts.Remove(item);
            }

            //Delete Product from baskets
            List<BasketProduct> basketProducts = _context.BasketProducts.Where(x => x.ProductId == id).ToList();
            foreach (BasketProduct item in basketProducts)
            {
                _context.BasketProducts.Remove(item);
            }

            Product p = _context.Products.FirstOrDefault(x => x.ProductId == id);

            //Delete Product image
            if(p.ImageId != 1)
            {
                Image i = _context.Images.FirstOrDefault(x => x.ImageId == p.ImageId);
                _context.Images.Remove(i);
            }

            //Delete Product
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
            Product product = _context.Products.Include(x => x.Image).FirstOrDefault(x => x.ProductId == id);
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
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Update(Product p, string[] category, AddImage image)
        {
            if (image.ImageUrl != null)
            {
                Image i = _context.Images.FirstOrDefault(i => i.ImageId == p.ImageId);

                if (i.ImageId == 1)
                {
                    i = new Image();
                }

                string name = image.ImageUrl.FileName.Split(".")[0];
                i.FileName = name;

                var extension = Path.GetExtension(image.ImageUrl.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                image.ImageUrl.CopyTo(stream);
                i.ImageUrl = newImageName;

                if (i.ImageId == 0)
                {
                    _context.Images.Add(i);
                    _context.SaveChanges();
                    p.ImageId = i.ImageId;
                }
                else
                {
                    _context.Images.Update(i);
                }
            }

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