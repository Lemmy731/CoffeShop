using CoffeeShop.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class ColdDrink: IEntityBase
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public string message { get; set; }
        public string version { get; set; }     

        [ForeignKey(nameof(Customer))]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }  
    }
}
