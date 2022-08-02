using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Aciktim.Areas.Client.Controllers
{
    [Area("Client")]
    public class AddressController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            dynamic myModal = new ExpandoObject();
            myModal.Address = _context.GetClientFullAddress(1).ToList();
            myModal.Country = _context.Countries.ToList();
            return View(myModal);
        }

        public IActionResult Delete(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            ClientAddress a = _context.ClientAddresses.FirstOrDefault(a => a.AddressId == id && a.ClientId == 1);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (a != null)
            {
                _context.ClientAddresses.Remove(a);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
