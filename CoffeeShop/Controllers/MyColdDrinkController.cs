using CoffeeShop.Models.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class MyColdDrinkController : Controller
    {
        private readonly IColdDrinkService _coldDrinkService;

        public MyColdDrinkController(IColdDrinkService coldDrinkService)
        {
                _coldDrinkService = coldDrinkService;   
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _coldDrinkService.GetColdDrink();
                return View(result); ;
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);  
            }
           
        }
    }
}
