using CoffeeShop.Data;

namespace CoffeeShop.Models.IServices
{
    public interface IHotDrinkService: IGenericRepository<HotDrink>
    {
        Task<List<HotDrink>> GetHotDrink();
    }
}
