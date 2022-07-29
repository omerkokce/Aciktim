using Microsoft.AspNetCore.Mvc;
using Aciktim.Models;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Aciktim.Areas.Client.Controllers
{
    public class RestaurantController : Controller
    {
        AciktimContext _context = new AciktimContext();
        [Area("Client")]
        public IActionResult Index(int id)
        {
            dynamic myModel = new ExpandoObject();
            myModel.Product = _context.Products.Where(p => p.RestaurantId == id).ToList();
            myModel.Comment = _context.Comments.Where(c => c.RestaurantId == id).Include(c => c.Client).ToList();
            myModel.Restaurant = _context.Restaurants.Where(r => r.RestaurantId == id).FirstOrDefault();
            myModel.Address = _context.GetRestaurantFullAddress(id).ToList();
            return View(myModel);
        }
    }
}
