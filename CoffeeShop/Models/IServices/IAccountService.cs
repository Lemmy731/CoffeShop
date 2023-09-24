using CoffeeShop.DataDTO;
using CoffeeShop.Helper;
//using System.Threading.Tasks;
using System;

namespace CoffeeShop.Models.IServices
{
    public interface IAccountService
    {
        Task<string> Register(RegisterDTO registerDTO);
        Task<Response<Tokens>> Login(LoginDTO loginDTO);
        Task<Response<Tokens>> Refresh(Tokens token, List<string> roles);
    }
}
