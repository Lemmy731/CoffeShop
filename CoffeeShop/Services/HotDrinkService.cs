using CoffeeShop.Data;
using CoffeeShop.Models;
using CoffeeShop.Models.IServices;

namespace CoffeeShop.Services
{
    public class HotDrinkService : GenericRepository<HotDrink>, IHotDrinkService
    {
        private readonly IGenericRepository<HotDrink> _genericReposistory;

        public HotDrinkService(AppDbContext appDbContext, IGenericRepository<HotDrink> genericRepository): base(appDbContext)
        {
                _genericReposistory = genericRepository;    
        }
        public async Task<List<HotDrink>> GetHotDrink()
        {
           var list =  await _genericReposistory.GetAllAsync();
            return list.ToList();
            
        }
    }
}
