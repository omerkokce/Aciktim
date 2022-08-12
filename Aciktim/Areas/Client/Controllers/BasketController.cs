using Microsoft.AspNetCore.Mvc;
using Aciktim.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Aciktim.Areas.Client.Controllers
{
    [Authorize(Policy = "Client")]
    [Area("Client")]
    public class BasketController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            ViewBag.id = id;
            List<Product> basket = _context.GetBasketProduct(id).ToList();
            return View(basket);
        }
        [Route("/Client/Basket/Delete/{productId}/{clientId}")]
        public IActionResult Delete(int productId, int clientId)
        {
            BasketProduct p = _context.BasketProducts.FirstOrDefault(p => p.ProductId == productId && p.ClientId == clientId);
            if (p != null)
            {
                _context.BasketProducts.Remove(p);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", new { id = clientId });
        }
    }
}
