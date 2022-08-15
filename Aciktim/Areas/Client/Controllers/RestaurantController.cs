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

            myModel.Product = _context.Products.Include(x => x.Image).Where(p => p.RestaurantId == id).ToList();
            myModel.Menu = _context.Menus.Include(x => x.Image).Where(m => m.RestaurantId == id).ToList();
            myModel.Comment = _context.Comments.Where(c => c.RestaurantId == id).Include(c => c.Client).ToList();
            myModel.Restaurant = _context.Restaurants.Include(x => x.Image).Where(r => r.RestaurantId == id).FirstOrDefault();
            myModel.Address = _context.GetRestaurantFullAddress(id).ToList();
            return View(myModel);
        }

        [Route("/Client/Restaurant/Add/{id}/{clientId}")]
        public string Add(int id, int clientId)
        {
            Product p = _context.Products.FirstOrDefault(p => p.ProductId == id);
            Models.Client c = _context.Clients.FirstOrDefault(c => c.ClientId == clientId);
            List<Product> products = _context.GetBasketProduct(clientId).ToList();
            List<Menu> menuList = _context.GetBasketMenu(clientId).ToList();
            if ((products.Count != 0 && products[0].RestaurantId != p.RestaurantId) || (menuList.Count != 0 && menuList[0].RestaurantId != p.RestaurantId))
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

        [Route("/Client/Restaurant/AddMenu/{id}/{clientId}")]
        public string AddMenu(int id, int clientId)
        {
            Menu m = _context.Menus.FirstOrDefault(m => m.MenuId == id);
            Models.Client c = _context.Clients.FirstOrDefault(c => c.ClientId == clientId);
            List<Product> products = _context.GetBasketProduct(clientId).ToList();
            List<Menu> menuList = _context.GetBasketMenu(clientId).ToList();
            if ((products.Count != 0 && products[0].RestaurantId != m.RestaurantId) || (menuList.Count != 0 && menuList[0].RestaurantId != m.RestaurantId))
            {
                return "fail";
            }

            if (m != null && c != null)
            {
                _context.BasketMenus.Add(new BasketMenu { Menu = m, Client = c });
                _context.SaveChanges();
                return "success";
            }
            return "fail";
        }
    }
}