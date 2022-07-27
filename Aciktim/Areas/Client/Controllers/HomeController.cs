using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        AciktimContext _context = new AciktimContext();
        [Area("Client")]

        public IActionResult Index()
        {
            List<Restaurant> r = _context.Restaurants.ToList();
            return View(r);
        }

    }
}
