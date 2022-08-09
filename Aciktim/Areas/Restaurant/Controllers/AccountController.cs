using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Areas.Restaurant.Controllers
{
    [Area("Restaurant")]
    public class AccountController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            Models.Restaurant restaurant = _context.Restaurants.FirstOrDefault(r => r.RestaurantId == 2);
            return View(restaurant);
        }

        public IActionResult Update(Models.Restaurant _restaurant)
        {
            if (_restaurant != null)
            {
                try
                {
                    _context.Restaurants.Update(_restaurant);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    RedirectToAction("Index","Home");
                    throw e;
                }
            }
            return RedirectToAction("Index");
        }
    }
}
