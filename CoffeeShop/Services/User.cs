using CoffeeShop.Data;
using CoffeeShop.DataDTO;
using CoffeeShop.Models;
using CoffeeShop.Models.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Services
{

    public class User : GenericRepository<UserRefreshToken>, IUser
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGenericRepository<UserRefreshToken> _genericRepository;
        private readonly UserManager<AppUser> _userManager;

        public User(AppDbContext appDbContext, IGenericRepository<UserRefreshToken> genericRepository, UserManager<AppUser> userManager ) : base(appDbContext)
        {
            _appDbContext = appDbContext;  
            _genericRepository = genericRepository; 
            _userManager = userManager; 
        }

        public async Task<string> AddUserRefreshTokens(UserRefreshToken refreshToken)
        {

           var response = await _genericRepository.AddAsync(refreshToken);
            if (response == "successfully added")
            {
                return "success";
            }
            return "unsuccess";
           
        }

        public void DeleteUserRefreshTokens(string userName, string refreshToken)
        {
            var decodeRefreshToken = Uri.UnescapeDataString(refreshToken);
            var item = _appDbContext.UserRefreshTokens.FirstOrDefault(x => x.UserName == userName && x.RefreshToken == decodeRefreshToken);
            if (item != null)
            {
                _genericRepository.DeleteAsync(item.Id);
            }
        }

        public UserRefreshToken GetSavedRefreshTokens(string username, string refreshtoken)
        {
            string decodedRefreshToken = Uri.UnescapeDataString(refreshtoken);

            // Query the database for the decoded refresh token
            var result = _appDbContext.UserRefreshTokens
                .Where(x => x.UserName == username && x.RefreshToken == decodedRefreshToken && x.IsActive == true)
                .FirstOrDefault(); // Assuming you want a single result
            var refresh = new UserRefreshToken
            {
                RefreshToken = result.RefreshToken
            };
            
            return refresh;
        }

        public async Task<bool> IsValidUserAsync(LoginDTO loginDTO)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == loginDTO.UserName);
            var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            return result;
        }

        public async Task<List<AppUser>> MyUser()
        {
            var list = await _appDbContext.Users.ToListAsync();
             return list;
        }

        public async Task<int> SaveCommit()
        {
            return await _appDbContext.SaveChangesAsync(); 
        }
    }
}
