using CoffeeShop.DataDTO;
using CoffeeShop.Services;

namespace CoffeeShop.Models.IServices
{
    public interface IUser
    {
        Task <List<AppUser>> MyUser();
        Task<bool> IsValidUserAsync(LoginDTO loginDTO);
        Task<string> AddUserRefreshTokens(UserRefreshToken user);
        UserRefreshToken GetSavedRefreshTokens(string username, string refreshtoken);
        void DeleteUserRefreshTokens(string username, string refreshToken);
        Task <int> SaveCommit();




    }
}