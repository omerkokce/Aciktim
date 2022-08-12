using Microsoft.AspNetCore.Mvc;
using Aciktim.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Aciktim.Areas.Client.Controllers
{
    [Authorize(Policy = "Client")]
    [Area("Client")]
    public class CardController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            ViewBag.id = id;
            List<Card> cards = _context.Cards.Where(c => c.ClientId == id).ToList();
            return View(cards);
        }

        [HttpPost]
        public IActionResult Add(Card c)
        {
            try
            {
                _context.Cards.Add(c);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = c.ClientId });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Home");
        }
        [Route("/Client/Card/Delete/{cardId}")]
        public IActionResult Delete(int cardId)
        {
            Card c = _context.Cards.FirstOrDefault(c => c.CardId == cardId);
            if (c != null)
            {
                _context.Cards.Remove(c);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = c.ClientId });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
