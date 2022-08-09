using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Aciktim.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        AciktimContext _context = new AciktimContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            Client client = _context.Clients.FirstOrDefault(x => x.EMail == model.EMail && x.Password == model.Password);

            if (client == null)
            {
                Restaurant restaurant = _context.Restaurants.FirstOrDefault(x => x.EMail == model.EMail && x.Password == model.Password);
                if (restaurant == null)
                {
                    Carrier carrier = _context.Carriers.FirstOrDefault(x => x.EMail == model.EMail && x.Password == model.Password);
                    if (carrier == null)
                    {
                        ViewBag.message = "Kullanıcı Adı veya şifre hatalı";
                        return View();
                    }
                    else
                    {
                        return Redirect("/Carrier");
                    }
                }
                else
                {
                    return Redirect("/Restaurant");
                }
            }
            else
            {
                return Redirect("/Client");
            }
        }
    }
}