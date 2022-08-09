using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Areas.Carrier.Controllers
{
    [Area("Carrier")]
    public class AccountController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            Models.Carrier carrier = _context.Carriers.FirstOrDefault(c => c.CarrierId == 5);
            return View(carrier);
        }

        public IActionResult Update(Models.Carrier carrier)
        {
            if (carrier != null)
            {
                try
                {
                    _context.Carriers.Update(carrier);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    RedirectToAction("Index", "Home");
                    throw e;
                }
            }
            return RedirectToAction("Index");
        }
    }
}
