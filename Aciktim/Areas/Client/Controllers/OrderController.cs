using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace Aciktim.Areas.Client.Controllers
{
    [Area("Client")]
    public class OrderController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            dynamic myModel = new ExpandoObject();
            myModel.Order = _context.Orders.Include(o => o.Restaurant).Where(o => o.ClientId == 1).ToList();
            myModel.Address = _context.GetOrderFullAddress(1);
            return View(myModel);
        }
    }
}