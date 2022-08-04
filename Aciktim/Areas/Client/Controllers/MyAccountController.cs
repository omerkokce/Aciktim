using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Areas.Client.Controllers
{
    [Area("Client")]
    public class MyAccountController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            Models.Client client = _context.Clients.FirstOrDefault(c => c.ClientId == 1);
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
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return RedirectToAction("Index");
        }
    }
}
