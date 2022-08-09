using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Security
{
    public class MyAuthorize : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
