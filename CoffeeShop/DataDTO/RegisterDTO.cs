using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CoffeeShop.DataDTO
{
    public class RegisterDTO
    {
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Display(Name ="UserName")]
        public string UserName { get; set; }        

        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "PhoneNumber required")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }
        [Display(Name = "Address")]                                                                                                                        
        [Required(ErrorMessage ="Address is required")]
        public string Address { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
