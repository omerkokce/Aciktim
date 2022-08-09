using Aciktim.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aciktim.Areas.Client.Controllers
{
    public class denemeController : Controller
    {
        AciktimContext _context = new AciktimContext();
        [Area("Client")]
        public IActionResult Index()
        {
            List<ProductCategory> asd = _context.ProductCategories.Where(x => x.ProductId == 1).ToList();
            List<int> asdf = new List<int>();
            foreach (ProductCategory item in asd)
            {
                asdf.Add(item.CategoryId);
            }
            ViewBag.qwe = asdf;
            return View();
        }
    }
}
