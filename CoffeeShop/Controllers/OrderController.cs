using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
