using Aciktim.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aciktim.Areas.Client.Controllers
{
    [Authorize(Policy = "Client")]
    [Area("Client")]
    public class HomeController : Controller
    {
        AciktimContext _context = new AciktimContext();

        public IActionResult Index()
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            int id = Convert.ToInt32(User.FindFirstValue("ClientId"));
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
