using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class OrderItem
    {
        
        [Key]
        public string Id { get; set; }
        public string Product  { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
       
        [ForeignKey(nameof(Order))]
        public string OrderId { get; set; }
        public Order Order { get; set; }
        
}
}
