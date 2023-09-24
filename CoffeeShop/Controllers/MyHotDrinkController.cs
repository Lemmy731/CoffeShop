using CoffeeShop.Models.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class MyHotDrinkController : Controller
    {
        private readonly IHotDrinkService _hotDrinkService;

        public MyHotDrinkController(IHotDrinkService hotDrinkService)
        {
                _hotDrinkService = hotDrinkService; 
        }
        public async Task<IActionResult> Index()
        {
            try
            {
               var result = await _hotDrinkService.GetHotDrink();
                return View(result);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);  
            }
           
        }
    }
}
