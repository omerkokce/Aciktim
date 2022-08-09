﻿using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aciktim.Areas.Restaurant.Controllers
{
    [Area("Restaurant")]
    public class HomeController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            ViewBag.Order = _context.Orders.Where(o => o.RestaurantId == 2).Include(o => o.Client).ToList();
            return View();
        }
    }
}
