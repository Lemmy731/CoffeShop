using CoffeeShop.Models;
using CoffeeShop.Models.IServices;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CoffeeShop.Services
{
    public class Authentication: IAuthentication
    {
        private readonly IConfiguration _configuration;
        private string _jwtSecret;
        private string _expires;
        private string _issuer;
        private string _audience;

        public Authentication()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _jwtSecret = _configuration["JWT:Secret"];
            _expires = _configuration["JWT:Expiress"];
            _issuer = _configuration["JWT:ValidIssuer"];
            _audience = _configuration["JWT:ValidAudience"];
        }

        public Tokens GenerateJWTTokens(string userName, List<string> roles)
        {
            return GenerateToken(userName, roles);
        }

        public Tokens GenerateNewToken(string userName, List<string> roles)
        {
            return GenerateToken(userName, roles);
        }

        public Tokens GenerateToken(string userName, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(5).ToString("o"))
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var credent = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.UtcNow.AddMinutes(5);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
               
            Issuer = _issuer,
            Audience = _audience,
            Subject = new ClaimsIdentity(claims),
            Expires = expire,
            SigningCredentials = credent
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = GenerateRefreshToken();
            return new Tokens { Access_Token = tokenHandler.WriteToken(token), Refresh_Token = refreshToken };
            

        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_jwtSecret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }


            return principal;
        }

    

    public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

       
    }
}
