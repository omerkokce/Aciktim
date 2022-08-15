using Aciktim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Aciktim.Areas.Restaurant.Controllers
{
    [Authorize(Policy = "Restaurant")]
    [Area("Restaurant")]
    public class AccountController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            ViewBag.id = id;
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            Models.Restaurant restaurant = _context.Restaurants.Include(x=>x.Image).FirstOrDefault(r => r.RestaurantId == id);
            return View(restaurant);
        }

        public IActionResult Update(Models.Restaurant _restaurant, AddImage image)
        {
            if (image.ImageUrl != null)
            {
                Image i = _context.Images.FirstOrDefault(i => i.ImageId == _restaurant.ImageId);

                if (i.ImageId == 1)
                {
                    i = new Image();
                }

                string name = image.ImageUrl.FileName.Split(".")[0];
                i.FileName = name;

                var extension = Path.GetExtension(image.ImageUrl.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                image.ImageUrl.CopyTo(stream);
                i.ImageUrl = newImageName;

                if (i.ImageId == 0)
                {
                    _context.Images.Add(i);
                    _context.SaveChanges();
                    _restaurant.ImageId = i.ImageId;
                }
                else
                {
                    _context.Images.Update(i);
                }
            }

            if (_restaurant != null)
            {
                try
                {
                    _context.Restaurants.Update(_restaurant);
                    _context.SaveChanges();
                    return RedirectToAction("Index", new { id = _restaurant.RestaurantId });
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
