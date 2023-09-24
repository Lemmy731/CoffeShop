using CoffeeShop.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.DataDTO
{
    public class CreateFoodDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public string CustomerId { get; set; }  
       
    }
}
