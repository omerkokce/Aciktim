using Aciktim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aciktim.Areas.Restaurant.Controllers
{
    [Authorize(Policy = "Restaurant")]
    [Area("Restaurant")]
    public class CarrierController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            ViewBag.id = id;
            List<Models.Carrier> list = _context.Carriers.ToList();
            ViewBag.Carrier = _context.GetRestaurantCarrier(id).ToList();
            return View(list);
        }

        public string Agreement(int id)
        {
            Models.Carrier c = _context.Carriers.FirstOrDefault(x => x.CarrierId == id);
            
            if (c != null)
            {
                
                _context.SaveChanges();
                return "success";
            }
            return "fail";
        }
    }
}
