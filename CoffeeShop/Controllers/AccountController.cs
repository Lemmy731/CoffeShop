using CoffeeShop.DataDTO;
using CoffeeShop.Models.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CoffeeShop.Models;
using System.Net.Http.Headers;
using System.Web.Razor.Parser;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Newtonsoft.Json;
using CoffeeShop.Data;

namespace CoffeeShop.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUser _user;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly HttpClient _httpClient;
        private HttpContext _httpContext;
        

        public AccountController(IAccountService accountService, IUser user, SignInManager<AppUser> signInManager, HttpClient httpClient)
        {
                _accountService = accountService;  
                _user = user;   
                _signInManager = signInManager; 
                 _httpClient = httpClient; 
                
                 
        }
        public IActionResult Index()
        {

          
            return View();
        }

        public async Task<IActionResult> MyUser()
        {
           var result = await _user.MyUser();
            return View(result);    
        }

        
        public IActionResult Register() => View(new RegisterDTO());

        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult>Register(RegisterDTO registerDTO)
        {
            try
            {
             
                if (ModelState.IsValid)
                {
                    var result = await _accountService.Register(registerDTO);
                    if (result == "user and customer created")
                        return View("RegistrationCompleted");
                    if(result == "user already exist with this email")
                    {
                        TempData["Error"] = "This email address is already in use";
                        return View(registerDTO);
                    }
                }
                return View(registerDTO);
            }
            catch (Exception ex) 
            {
                 return BadRequest(ex.Message);  
           
            }
        }

        public IActionResult Login() => View(new LoginDTO());


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("/LogIn")]
        public async Task<IActionResult> LogIn(LoginDTO loginDTO, string returnUrl)
        {

           try
            {
                if (!ModelState.IsValid)
                {
                    return View(loginDTO);
                }

                var token = await _accountService.Login(loginDTO);

                UserRefreshToken obj = new UserRefreshToken
                {
                    Id = Guid.NewGuid().ToString(), 
                    UserName = loginDTO.UserName,
                    RefreshToken = token.RefreshToken,
                    IsActive = true
                };

                await _user.AddUserRefreshTokens(obj);
                await _user.SaveCommit(); 

                if (token.Message == "user not found" || token.Message == "incorrect password")
                {
                    TempData["ErrorMessage"] = "Invalid username or password.";
                    return View(loginDTO);
                }

                // Store token in a secure way, such as HttpOnly cookies
                Response.Cookies.Append("Access_Token", token.AccessToken, new CookieOptions
                {
                    HttpOnly = false,
                    Expires = DateTime.UtcNow.AddMinutes(5) // Adjust expiration time as needed
                });
                Response.Cookies.Append("Refresh_Token", token.RefreshToken
                    , new CookieOptions
                {
                    HttpOnly = false,
                    Expires = DateTime.UtcNow.AddMinutes(6) // Adjust expiration time as needed
                });

                // Redirect user based on successful login
                if (returnUrl != "direct login")
                {
                    return LocalRedirect(returnUrl);
                }

                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Index", "Home");
                
            }
            catch (Exception ex)
            {
                // Log the exception and handle accordingly
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("/LogIn")]
        public async Task<IActionResult> Refresh(Tokens token, List<string> roles, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var tokens = await _accountService.Refresh(token, roles);

                if (tokens.Message == "Invalid attempt!" || tokens.Message == "something went wrong!")
                {
                    TempData["ErrorMessage"] = "Invalid username or password.";
                    return View();
                }

                // Store token in a secure way, such as HttpOnly cookies
                Response.Cookies.Append("Access_Token", tokens.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddMinutes(10) // Adjust expiration time as needed
                });
                 Response.Cookies.Append("Refresh_Token", tokens.RefreshToken
                    , new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTime.UtcNow.AddMinutes(15) // Adjust expiration time as needed
                    });

               

                // Redirect user based on successful login
                if (returnUrl != "direct login")
                {
                    return LocalRedirect(returnUrl);
                }

                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                // Log the exception and handle accordingly
                return BadRequest(ex.Message);
            }
        }



        public async Task<IActionResult> LogOut()
        {
           await _signInManager.SignOutAsync();

            TempData["SuccessMessage"] = "signed out!";
            return RedirectToAction("LogIn", "Account");   
        }
    }
}
