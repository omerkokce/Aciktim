using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Areas.Client.Controllers
{
    public class AddressController : Controller
    {
        AciktimContext _context = new AciktimContext();
        [Area("Client")]
        public IActionResult Index()
        {
            List<GetAddress> addresses = _context.GetClientFullAddress(1).ToList();
            return View(addresses);
        }
    }
}
