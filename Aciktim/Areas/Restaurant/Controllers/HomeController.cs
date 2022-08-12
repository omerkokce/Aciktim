using Aciktim.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Aciktim.Areas.Restaurant.Controllers
{
    [Authorize(Policy = "Restaurant")]
    [Area("Restaurant")]
    public class HomeController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            int id = Convert.ToInt32(User.FindFirstValue("RestaurantId"));
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            ViewBag.id = id;
            ViewBag.Order = _context.Orders.Where(o => o.RestaurantId == id).Include(o => o.Client).ToList();
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        public IActionResult SendOrder(int id)
        {
            Order o = _context.Orders.FirstOrDefault(x => x.OrderId == id);
            if (o != null)
            {
                o.IsActive = false;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
