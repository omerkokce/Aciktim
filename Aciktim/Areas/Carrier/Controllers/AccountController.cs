using Aciktim.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aciktim.Areas.Carrier.Controllers
{
    [Authorize(Policy = "Carrier")]
    [Area("Carrier")]
    public class AccountController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            ViewBag.id = id;
            Models.Carrier carrier = _context.Carriers.FirstOrDefault(c => c.CarrierId == id);
            return View(carrier);
        }

        public IActionResult Update(Models.Carrier carrier)
        {
            try
            {
                _context.Carriers.Update(carrier);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = carrier.CarrierId });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
