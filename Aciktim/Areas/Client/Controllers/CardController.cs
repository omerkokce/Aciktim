using Microsoft.AspNetCore.Mvc;
using Aciktim.Models;

namespace Aciktim.Areas.Client.Controllers
{
    public class CardController : Controller
    {
        AciktimContext _context = new AciktimContext();
        [Area("Client")]
        public IActionResult Index()
        {
            List<Card> cards = _context.Cards.Where(c => c.ClientId == 1).ToList();
            return View(cards);
        }
    }
}
