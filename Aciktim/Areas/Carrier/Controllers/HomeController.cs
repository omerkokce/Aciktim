using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Aciktim.Areas.Carrier.Controllers
{
    [Area("Carrier")]
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
