using CoffeeShop.Data;
using CoffeeShop.Models;
using CoffeeShop.Models.IServices;

namespace CoffeeShop.Services
{
    public class ColdDrinkService : GenericRepository<ColdDrink>, IColdDrinkService
    {
        private readonly IGenericRepository<ColdDrink> _genericRepository;
        private readonly AppDbContext _appDbContext;

        public ColdDrinkService(AppDbContext appDbContext,   IGenericRepository<ColdDrink> genericRepository) : base(appDbContext)
        {
                _genericRepository = genericRepository; 
                _appDbContext = appDbContext;   
        }
        public async Task<List<ColdDrink>> GetColdDrink()
        {
            var list = await _genericRepository.GetAllAsync();
            return list.ToList();
        }
    }
}
