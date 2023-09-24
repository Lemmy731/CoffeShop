using System.Security.Claims;

namespace CoffeeShop.Models.IServices
{
    public interface IAuthentication
    {
        Tokens GenerateToken(string userName, List<string> roles);
        //string ValidateToken(string token);
        Tokens GenerateNewToken(string userName, List<string> roles);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Tokens GenerateJWTTokens(string userName, List<string> roles);
    }
}
