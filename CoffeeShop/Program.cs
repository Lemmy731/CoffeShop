using CoffeeShop.Config;
using CoffeeShop.Controllers;
using CoffeeShop.Data;
using CoffeeShop.Models;
using CoffeeShop.Models.IServices;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.IdentityModel.Tokens;
using System.Resources;
using System.Runtime.Intrinsics.X86;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;
var configur = builder.Configuration;

var jwtCon = configur.GetSection("JWT").Get<JwtConfiguration>();




// Add services to the container.
builder.Services.AddControllersWithViews();
service.AddSingleton(builder.Configuration);
service.AddDbContext<AppDbContext>(options =>options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString")));
service.AddIdentity<AppUser, IdentityRole>(
    options =>
    {
        options.Password.RequireUppercase = true; // on production add more secured options
        options.Password.RequireDigit = true;
        //options.SignIn.RequireConfirmedEmail = true;
    }
    )
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
service.AddScoped<UserManager<AppUser>>();
service.AddScoped<SignInManager<AppUser>>();
service.AddHttpContextAccessor();

service.AddScoped<IAccountService, AccountService>();
service.AddScoped<IOrderService, OrderService>();
service.AddScoped<IGenerateReceiptService, GenerateReceiptService>();
service.AddScoped<IAuthentication, Authentication>();
service.AddScoped<IMealService, MealService>();
service.AddScoped<IColdDrinkService, ColdDrinkService>();
service.AddScoped<IHotDrinkService, HotDrinkService>();
service.AddScoped<IUser, User>();
service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
 .BuildServiceProvider();
service.AddHttpClient();
service.AddHttpContextAccessor
    ();
service.AddTransient<CheckAccessTokenValidity>();

// ... Other configurations ...


service.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.LoginPath = "Account/LogIn"; // Customize the login path
       })
.AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtCon.ValidIssuer,
            ValidAudience = jwtCon.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtCon.Secret)),
          // ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                }
                return Task.CompletedTask;
            }
        };

    });

//service.AddAuthorization(options =>
//{
//    //options.FallbackPolicy = new AuthorizationPolicyBuilder()
//    //    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme)
//       //.RequireAuthenticatedUser() 
//        //.Build(); 
//});


// service.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(10);
//});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

//app.Use(async (context, next) =>
//{
//    try
//    {
//        await next();
//    }
//    catch (SecurityTokenExpiredException)
//    {
//        context.Response.StatusCode = 401;
//        context.Response.ContentType = "application/json";
//        await context.Response.WriteAsync("{\"message\":\"Token has expired.\"}");
//    }
//    catch (SecurityTokenValidationException)
//    {
//        context.Response.StatusCode = 401;
//        context.Response.ContentType = "application/json";
//        await context.Response.WriteAsync("{\"message\":\"Token validation failed.\"}");
//    }
//});



app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CheckAccessTokenValidity>();



app.UseEndpoints(endpoints =>
{
    //endpoints.MapControllerRoute(
    //    name: "api",
    //    pattern: "api/{controller}/{action}/{id?}");

    //endpoints.MapControllerRoute(name: "default",
    //pattern: "{controller}/{action}/{id?}");

    endpoints.MapControllerRoute(name: "default",   
        pattern: "{controller=Home}/{action=Index}/{id?}");


});

//app.UseSession();   

SeedData.Seed(app);
SeedData.SeedUserAndRoleAsync(app).Wait();


app.Run();

