using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Aciktim.Areas.Client.Controllers
{
    [Area("Client")]
    public class PaymentController : Controller
    {
        AciktimContext _context = new AciktimContext();
        [HttpGet]
        [Route("Client/Payment/Index/{prices}")]
        public IActionResult Index(decimal prices)
        {
            dynamic myModel = new ExpandoObject();
            myModel.Address = _context.GetClientFullAddress(1).ToList();
            myModel.Cards = _context.Cards.Where(c => c.ClientId == 1).ToList();
            myModel.Price = prices;
            return View(myModel);
        }
    }
}
