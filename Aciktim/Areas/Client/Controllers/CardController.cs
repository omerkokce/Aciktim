using Microsoft.AspNetCore.Mvc;
using Aciktim.Models;

namespace Aciktim.Areas.Client.Controllers
{
    [Area("Client")]
    public class CardController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            List<Card> cards = _context.Cards.Where(c => c.ClientId == 1).ToList();
            return View(cards);
        }
        [HttpPost]
        public IActionResult Add(Card c)
        {
            try
            {
                _context.Cards.Add(c);
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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Card c = _context.Cards.FirstOrDefault(c => c.CardId == id && c.ClientId == 1);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (c != null)
            {
                _context.Cards.Remove(c);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
