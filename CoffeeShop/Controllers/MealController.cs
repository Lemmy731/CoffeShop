using CoffeeShop.Config;
using CoffeeShop.DataDTO;
using CoffeeShop.Models;
using CoffeeShop.Models.IServices;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web.WebPages;

namespace CoffeeShop.Controllers
{

    
    public class MealController : Controller
    {
            

        private IMealService _mealService;
        private readonly IHttpContextAccessor _httpContextAssessor;

        public MealController(IMealService mealService, IHttpContextAccessor httpContextAccessor)
        {
                _mealService = mealService;
            _httpContextAssessor = httpContextAccessor;     
           
            
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            try
            {
                var result = await _mealService.GetAllMeal();
                return View(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }

           
        }

        //public IActionResult CreateMeal() => View(new CreateFoodDTO());

        public IActionResult CreateMeal()
        {
            var Access_Token = Request.Cookies["Access_Token"];
            ViewBag.Access_Token = Access_Token; // Pass the JWT token to the view

            return View();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]
        [HttpPost]
        [Route("Meal/CreateMeal")]
       
        public async Task<IActionResult> CreateMeal(CreateFoodDTO foodDto)
        {

                 

            if (!ModelState.IsValid)
            {
                return View(foodDto);
            }


            try
            {
                if (foodDto != null)
                {

                    var result = await _mealService.MealCreate(foodDto);
                    if (result == "successfully create meal")
                    {
                        return View("MealReturnResult");
                    }

                }
                return View("MealReturnResult");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var result = await _mealService.Details(id);
                return View(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
              
        }
    }
}
