using Aciktim.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            List<Models.Restaurant> restaurants = _context.Restaurants.Include(x => x.Image).ToList();
            ViewBag.Restaurant = _context.GetFavoriteRestaurant(id).ToList();
            return View(restaurants);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [Route("/Client/Home/AddFavorite/{restaurantId}/{clientId}")]
        public IActionResult AddFavorite(int restaurantId, int clientId)
        {
            ClientFavorite favorite = new ClientFavorite { RestaurantId = restaurantId, ClientId = clientId };
            _context.ClientFavorites.Add(favorite);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("/Client/Home/DeleteFavorite/{restaurantId}/{clientId}")]
        public IActionResult DeleteFavorite(int restaurantId, int clientId)
        {
            ClientFavorite favorite = _context.ClientFavorites.FirstOrDefault(x => x.ClientId == clientId && x.RestaurantId == restaurantId);
            _context.ClientFavorites.Remove(favorite);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
