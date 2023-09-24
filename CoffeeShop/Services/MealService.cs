using CoffeeShop.Data;
using CoffeeShop.DataDTO;
using CoffeeShop.Models;
using CoffeeShop.Models.IServices;

namespace CoffeeShop.Services
{
    public class MealService : GenericRepository<Food>, IMealService
    {
        private AppDbContext _appDbContext;
        private readonly IGenericRepository<Food> _genericRepository;

        public MealService(AppDbContext appDbContext, IGenericRepository<Food> genericRepository): base(appDbContext) 
        {
                _appDbContext = appDbContext;  
                _genericRepository = genericRepository; 
        }
        public async Task<string> MealCreate(CreateFoodDTO createFoodDTO)
        {
            if(createFoodDTO != null)
            {
                Food food = new Food()
                {
                    Id = Guid.NewGuid().ToString(),    
                    Name = createFoodDTO.Name,  
                    Description = createFoodDTO.Description,    
                    Price = createFoodDTO.Price,    
                    ImageURL = createFoodDTO.ImageURL,  
                    CustomerId = createFoodDTO.CustomerId,  
                };
               var item = _genericRepository.AddAsync(food);
                if(item != null) 
                {
                   var resp = await _appDbContext.SaveChangesAsync(); 
                    
                    if(resp == 0) 
                    {
                        return "successfully create meal";
                    }
                }
             

            }
            return "no item found";
        }

        public async Task<List<Food>> GetAllMeal()
        {
           var list = await _genericRepository.GetAllAsync();
            return list.ToList();   
        }

        public async Task<Food> Details(string id)
        {
           var item = await _genericRepository.GetByIdAsync(id);
            return item;    
        }
    }
}
