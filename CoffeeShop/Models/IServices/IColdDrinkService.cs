using CoffeeShop.Data;

namespace CoffeeShop.Models.IServices
{
    public interface IColdDrinkService: IGenericRepository<ColdDrink> 
    {
        Task<List<ColdDrink>> GetColdDrink();
    }
}
