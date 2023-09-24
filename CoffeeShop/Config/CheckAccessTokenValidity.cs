using CoffeeShop.Models;
using CoffeeShop.Models.IServices;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Security.Claims;

namespace CoffeeShop.Config
{
    public class CheckAccessTokenValidity: IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _context;

        public CheckAccessTokenValidity()
        {
                
        }
        public CheckAccessTokenValidity(RequestDelegate next, IConfiguration configuration, IHttpContextAccessor context)
        {
            _next = next;
            _configuration = configuration;
            _context = context;
        }

        public CheckAccessTokenValidity(IAccountService accountservice)
        {
                _accountService = accountservice;   
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

               var accountService = _accountService;
         
              if(context.User.FindFirstValue("exp") != null)
            {

                string accessTokenCookieName = context.Request.Headers.Authorization;
                string clientAccessToken = accessTokenCookieName.Substring(6).Trim();
                string refreshTokenCookieName = context.Request.Headers.Cookie;

                var refresh = new Dictionary<string, string>();
                if (refreshTokenCookieName.Contains("Refresh_Token"))
                {

                    var value = refreshTokenCookieName.Split(';');
                    var reff = value[3].Split('=');
                    refresh.Add(reff[0], reff[1]);
                }
                string clientRefreshToken = refresh.Values.FirstOrDefault();

                var user = context.User;

                if (user.Identity.IsAuthenticated)
                {

                    // Use the tokens and refreshToken for token refresh logic
                    var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                    var refreshTokens = new Tokens { Access_Token = clientAccessToken, Refresh_Token = clientRefreshToken };

                    // Check if the access token is about to expire within 10 minutes
                    var accessTokenExpiration = context.User.FindFirstValue("exp");
                    if (int.TryParse(accessTokenExpiration, out int timestamp))
                    {
                        var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        var expireAt = unixEpoch.AddSeconds(timestamp);
                        var timeUntilExpiration = expireAt - DateTime.UtcNow;
                        if (timeUntilExpiration.TotalMinutes <= 4)
                        {
                            // Call your Refresh endpoint logic here
                            var refreshedTokens = await accountService.Refresh(refreshTokens, roles);

                            if (!string.IsNullOrEmpty(refreshedTokens.AccessToken))
                            {
                                // Store the new access token
                                context.Response.Cookies.Append("Access_Token", refreshedTokens.AccessToken, new CookieOptions
                                {
                                    HttpOnly = true,
                                    Expires = DateTime.UtcNow.AddMinutes(5)
                                });

                                context.Response.Cookies.Append("Refresh_Token", refreshedTokens.RefreshToken, new CookieOptions
                                {
                                    HttpOnly = true,
                                    Expires = DateTime.UtcNow.AddMinutes(6)
                                });
                            }
                        }
                    }
                }
            }

            await next.Invoke(context);
        }

       
    }
}
