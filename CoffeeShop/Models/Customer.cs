using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class Customer 
    {
        [Key]
        public string Id { get; set; }        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public string Address { get; set; }

        [ForeignKey(nameof(AppUser))]
        public string AppUserId { get; set; }   
        public AppUser AppUser { get; set; }

        public ICollection<Food> FoodPlates { get; set; }
        public ICollection<ColdDrink> ColdDrinks { get; set; }
        public ICollection<HotDrink> HotDrinks { get; set; }
        public ICollection<Order> Orders { get; set; }


    }
}
