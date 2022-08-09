using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Areas.Restaurant.Controllers
{
    [Area("Restaurant")]
    public class CarrierController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            List<Models.Carrier> list = _context.GetRestaurantCarrier(2).ToList();
            return View(list);
        }
    }
}
