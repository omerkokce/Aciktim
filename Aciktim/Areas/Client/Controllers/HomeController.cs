using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        AciktimContext _context = new AciktimContext();

        public IActionResult Index()
        {
            List<Models.Restaurant> restaurants = _context.Restaurants.ToList();
            return View(restaurants);
        }

    }
}
