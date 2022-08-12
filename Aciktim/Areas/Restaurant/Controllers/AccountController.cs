﻿using Aciktim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            Models.Restaurant restaurant = _context.Restaurants.FirstOrDefault(r => r.RestaurantId == id);
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
