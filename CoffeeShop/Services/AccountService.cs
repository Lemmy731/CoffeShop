using CoffeeShop.Data;
using CoffeeShop.DataDTO;
using CoffeeShop.Helper;
using CoffeeShop.Models;
using CoffeeShop.Models.IServices;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using System.Security.Claims;

namespace CoffeeShop.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _appDbContext;
        private readonly IAuthentication _authentication;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUser _user;
        private readonly HttpContext httpContext1;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext appDbContext, IAuthentication authentication, IHttpContextAccessor httpContext, IUser user)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
            _authentication = authentication;
            _httpContext = httpContext;
            _user = user;   


        }

        public async Task<string> Register(RegisterDTO registerDTO)
        {

            var user = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (user != null)
            {
                return "user already exist with this email";
            }

            AppUser appUser = new AppUser()
            {
                UserName = registerDTO.UserName,
                PhoneNumber = registerDTO.PhoneNumber,
                PasswordHash = registerDTO.Password,
                FullName = registerDTO.FullName,
                Email = registerDTO.Email
            };

            var userResponse = await _userManager.CreateAsync(appUser, appUser.PasswordHash);
            if (userResponse.Succeeded)
            {
                var roleResponse = await _userManager.AddToRoleAsync(appUser, UserRoles.CustomerRole);

                if (roleResponse.Succeeded)
                {
                    var saveResult = await _appDbContext.SaveChangesAsync();
                    
                }


                Customer customer = new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    IsDeleted = false,
                    UpdatedAt = DateTime.Now,
                    Address = registerDTO.Address,
                    AppUserId = appUser.Id

                };
                var customerObject = await _appDbContext.Customers.AddAsync(customer);
                if (customerObject != null)
                {
                    var saveResult = await _appDbContext.SaveChangesAsync();

                }

                return "user and customer created";
            }

            return "unable to create user and customer";
        }

        public async Task<Response<Tokens>> Login(LoginDTO loginDTO)
        {


            var user = await _userManager.FindByNameAsync(loginDTO.UserName);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (passwordCheck == false)
                {
                    return new Response<Tokens>
                    {
                        Message = "incorrect password"
                    };
                }
                if (passwordCheck == true)
                {
                    var roles = new List<string> { "Customer" };
                    var jwtToken = _authentication.GenerateToken(loginDTO.UserName, roles);
                    var signIn = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, true);
                    if (signIn.Succeeded)
                    {


                        return new Response<Tokens>
                        {
                            AccessToken = jwtToken.Access_Token,
                            RefreshToken = jwtToken.Refresh_Token
                            
                        };
                    }
                    
                }
            }
            return new Response<Tokens>
            {
                Message = "user not found"
            };
      
        }

        public async Task<Response<Tokens>> Refresh(Tokens token, List<string> roles)
        {
            var principal = _authentication.GetPrincipalFromExpiredToken(token.Access_Token);
            var username1 = principal.Claims.Where(x => x.Equals("nameidentifier"));
                var username3 = principal.Identity.Name;
            HttpContext context = _httpContext.HttpContext;
                    
            var username = context.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            var decodeBrowserRefreshToken = Uri.UnescapeDataString(token.Refresh_Token);

            //retrieve the saved refresh token from database
            var savedRefreshToken = _user.GetSavedRefreshTokens(username, token.Refresh_Token);

            if (savedRefreshToken.RefreshToken != decodeBrowserRefreshToken)
            {
                return new Response<Tokens>
                {
                    Message = "Invalid attempt!"
                };
                    
            }

            var newJwtToken = _authentication.GenerateNewToken(username, roles);

            if (newJwtToken == null)
            {
                return new Response<Tokens>
                {
                    Message = "something went wrong!"
                };
            }


                // saving refresh token to the db
                UserRefreshToken obj = new UserRefreshToken
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = username,
                    RefreshToken = newJwtToken.Refresh_Token,
                    IsActive = true    
                   
                };

                _user.DeleteUserRefreshTokens(username, token.Refresh_Token);
               var result = await _user.AddUserRefreshTokens(obj);
             if(result == "success")
            {
                await _user.SaveCommit();
            }
                

                return new Response<Tokens>
                {
                    AccessToken = newJwtToken.Access_Token,
                    RefreshToken = newJwtToken.Refresh_Token
                };

        }

            
        
    }
}

