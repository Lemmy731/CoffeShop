using CoffeeShop.Models;
using CoffeeShop.Models.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoffeeShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMealService _mealService;

        public HomeController(ILogger<HomeController> logger, IMealService mealService)
        {
            _logger = logger;
            _mealService = mealService;     
        }

        public async Task<IActionResult> Index()
        {
           var result = await _mealService.GetAllAsync();
            return View(result);
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
    }
}