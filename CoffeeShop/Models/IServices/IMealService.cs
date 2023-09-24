using CoffeeShop.Data;
using CoffeeShop.DataDTO;

namespace CoffeeShop.Models.IServices
{
    public interface IMealService: IGenericRepository<Food>
    {
        Task<string> MealCreate(CreateFoodDTO createFoodDTO);
        Task<List<Food>> GetAllMeal();
        Task<Food> Details(string id);


    }
}
