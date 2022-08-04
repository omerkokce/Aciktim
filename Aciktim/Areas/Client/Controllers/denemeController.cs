using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Areas.Client.Controllers
{
    public class denemeController : Controller
    {
        [Area("Client")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
