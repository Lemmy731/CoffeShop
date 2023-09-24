using CoffeeShop.Data;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class UserRefreshToken : IEntityBase
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
