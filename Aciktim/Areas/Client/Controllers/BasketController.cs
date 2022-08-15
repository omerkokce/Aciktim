using Microsoft.AspNetCore.Mvc;
using Aciktim.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Dynamic;

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

            dynamic myModel = new ExpandoObject();

            myModel.Product = _context.GetBasketProduct(id).ToList();
            myModel.Menu = _context.GetBasketMenu(id).ToList();

            return View(myModel);
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

        [Route("/Client/Basket/DeleteMenu/{menuId}/{clientId}")]
        public IActionResult DeleteMenu(int menuId, int clientId)
        {
            BasketMenu m = _context.BasketMenus.FirstOrDefault(p => p.MenuId == menuId && p.ClientId == clientId);
            if (m != null)
            {
                _context.BasketMenus.Remove(m);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", new { id = clientId });
        }
    }
}
