using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class Order
    {
        [Key]
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public DateTime OrderDate { get; set; } = DateTime.Now; 
        public double TotalAmount { get; set; }
        public string OrderReceipt { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        [ForeignKey(nameof(Customer))]
        public string CustomerId { get; set; }

        public Customer Customer { get; set; }
        
    }
}
