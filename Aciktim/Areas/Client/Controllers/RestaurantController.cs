using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Areas.Client.Controllers
{
    public class RestaurantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
