using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class Register
    {
        [Required(ErrorMessage = "Username can't be blank!")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email can't be blank!")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format!")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone can't be blank!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain numbers only!")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Password can't be blank!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password can't be blank!")]
        [Compare("Password", ErrorMessage = "Password and confirm password don't match!")]
        public string? ConfirmPassword { get; set; }
    }
}
