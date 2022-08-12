using Aciktim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aciktim.Areas.Restaurant.Controllers
{
    [Authorize(Policy = "Restaurant")]
    [Area("Restaurant")]
    public class ManagerController : Controller
    {
        AciktimContext _context = new AciktimContext();

        public IActionResult Index(int id)
        {
            ViewBag.id = id;
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;

            Models.Restaurant restaurant = _context.Restaurants.FirstOrDefault(r => r.RestaurantId == id);
            Manager manager = _context.Managers.FirstOrDefault(m => m.ManagerId == restaurant.ManagerId);

            return View(manager);
        }

        [Route("/Restaurant/Manager/Update/{_id}")]
        [HttpPost]
        public IActionResult Update(Manager manager, int _id)
        {
            if (manager != null)
            {
                _context.Managers.Update(manager);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = _id });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
