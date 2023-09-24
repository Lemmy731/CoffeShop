using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
//using System;

namespace CoffeeShop.DataDTO
{
    public class LoginDTO
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Password is required")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       
    }
}
