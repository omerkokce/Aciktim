using Aciktim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Areas.Client.Controllers
{
    [Authorize(Policy = "Client")]
    [Area("Client")]
    public class CommentController : Controller
    {
        AciktimContext _context = new AciktimContext();
        [Route("/Client/Comment/Index/{ClientId}/{RestaurantId}")]
        public IActionResult Index(int ClientId, int RestaurantId)
        {
            ViewBag.ClientId = ClientId;
            ViewBag.RestaurantId = RestaurantId;
            return View();
        }

        public IActionResult Add(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
