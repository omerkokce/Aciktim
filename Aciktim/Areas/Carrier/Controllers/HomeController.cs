using Aciktim.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Aciktim.Areas.Carrier.Controllers
{
    [Authorize(Policy = "Carrier")]
    [Area("Carrier")]
    public class HomeController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            int id = Convert.ToInt32(User.FindFirstValue("CarrierId"));
            ViewBag.id = id;

            List<Models.Restaurant> restaurants = _context.Restaurants.ToList();
            return View(restaurants);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
