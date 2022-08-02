using Microsoft.AspNetCore.Mvc;
using Aciktim.Models;

namespace Aciktim.Areas.Client.Controllers
{
    [Area("Client")]
    public class BasketController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            List<Product> basket = _context.GetBasketProduct(1).ToList();
            return View(basket);
        }
        public IActionResult Delete(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            BasketProduct p = _context.BasketProducts.FirstOrDefault(p => p.ProductId == id && p.ClientId == 1);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (p != null)
            {
                _context.BasketProducts.Remove(p);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
