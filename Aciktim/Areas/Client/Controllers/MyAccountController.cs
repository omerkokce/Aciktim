using Aciktim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aciktim.Areas.Client.Controllers
{
    [Authorize(Policy = "Client")]
    [Area("Client")]
    public class MyAccountController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            ViewBag.id = id;
            Models.Client client = _context.Clients.FirstOrDefault(c => c.ClientId == id);
            return View(client);
        }

        public IActionResult Update(Models.Client _client)
        {
            if (_client != null)
            {
                try
                {
                    _context.Clients.Update(_client);
                    _context.SaveChanges();
                    return RedirectToAction("Index", new { id = _client.ClientId });
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return RedirectToAction("Index","Home");
        }
    }
}
