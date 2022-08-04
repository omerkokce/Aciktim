using Microsoft.AspNetCore.Mvc;
using Aciktim.Models;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace Aciktim.Areas.Client.Controllers
{
    [Area("Client")]
    public class RestaurantController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            dynamic myModel = new ExpandoObject();
            myModel.Product = _context.Products.Where(p => p.RestaurantId == id).ToList();
            myModel.Comment = _context.Comments.Where(c => c.RestaurantId == id).Include(c => c.Client).ToList();
            myModel.Restaurant = _context.Restaurants.Where(r => r.RestaurantId == id).FirstOrDefault();
            myModel.Address = _context.GetRestaurantFullAddress(id).ToList();
            return View(myModel);
        }

        public string Add(int id)
        {
            Product p = _context.Products.FirstOrDefault(p => p.ProductId == id);
            Models.Client c = _context.Clients.FirstOrDefault(c => c.ClientId == 1);

            if (p != null && c != null)
            {
                _context.BasketProducts.Add(new BasketProduct { Product = p, Client = c });
                _context.SaveChanges();
                return "success";
            }
            return "fail";
        }
    }
}