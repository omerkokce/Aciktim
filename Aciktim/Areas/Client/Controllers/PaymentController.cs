using Aciktim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Security.Claims;

namespace Aciktim.Areas.Client.Controllers
{
    [Authorize(Policy = "Client")]
    [Area("Client")]
    public class PaymentController : Controller
    {
        AciktimContext _context = new AciktimContext();
        [HttpGet]
        [Route("Client/Payment/Index/{prices}/{clientId}")]
        public IActionResult Index(decimal prices,int clientId)
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            ViewBag.id = clientId;
            dynamic myModel = new ExpandoObject();
            myModel.Address = _context.GetClientFullAddress(clientId).ToList();
            myModel.Cards = _context.Cards.Where(c => c.ClientId == clientId).ToList();
            myModel.Price = prices;

            Product p = _context.GetBasketProduct(clientId).ToList()[0];
            ViewBag.RestaurantId = p.RestaurantId;

            if(myModel.Address.Count == 0)
            {
                return Redirect("/Client/Address/Index/" + clientId);
            }

            return View(myModel);
        }

        [HttpPost]
        public IActionResult Pay(Order order)
        {
            if(order != null)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                List<Product> products = _context.GetBasketProduct(order.ClientId).ToList();
                foreach (Product item in products)
                {
                    _context.OrderProducts.Add(new OrderProduct { OrderId = order.OrderId, ProductId = item.ProductId });
                    BasketProduct a = _context.BasketProducts.FirstOrDefault(p => p.ProductId == item.ProductId && p.ClientId == order.ClientId);
                    _context.BasketProducts.Remove(a);
                }
                _context.SaveChanges();
                return Redirect("/Client/Comment/Index/" + order.ClientId + "/" + order.RestaurantId);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
