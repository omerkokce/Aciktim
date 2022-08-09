using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Areas.Client.Controllers
{
    [Area("Client")]
    public class CommentController : Controller
    {
        AciktimContext _context = new AciktimContext();
        [Route("/Client/Comment/Index/{id1}/{id2}")]
        [Route("/Client/Comment/Index")]
        [Route("/Client/Comment")]
        public IActionResult Index(int ClientId, int RestaurantId)
        {
            ViewBag.ClientId = ClientId;
            ViewBag.RestaurantId = RestaurantId;
            return View();
        }
    }
}
