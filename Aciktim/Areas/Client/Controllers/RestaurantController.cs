using Microsoft.AspNetCore.Mvc;
using Aciktim.Models;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Aciktim.Areas.Client.Controllers
{
    [Authorize(Policy = "Client")]
    [Area("Client")]
    public class RestaurantController : Controller
    {
        AciktimContext _context = new AciktimContext();
        [Route("/Client/Restaurant/Index/{id}/{clientId}")]
        public IActionResult Index(int id, int clientId)
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            ViewBag.id = clientId;
            dynamic myModel = new ExpandoObject();
            myModel.Product = _context.Products.Where(p => p.RestaurantId == id).ToList();
            myModel.Comment = _context.Comments.Where(c => c.RestaurantId == id).Include(c => c.Client).ToList();
            myModel.Restaurant = _context.Restaurants.Where(r => r.RestaurantId == id).FirstOrDefault();
            myModel.Address = _context.GetRestaurantFullAddress(id).ToList();
            return View(myModel);
        }

        [Route("/Client/Restaurant/Add/{id}/{clientId}")]
        public string Add(int id, int clientId)
        {
            Product p = _context.Products.FirstOrDefault(p => p.ProductId == id);
            Models.Client c = _context.Clients.FirstOrDefault(c => c.ClientId == clientId);
            List<Product> products = _context.GetBasketProduct(clientId).ToList();
            if (products.Count != 0 && products[0].RestaurantId != id)
            {
                return "fail";
            }

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