using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email can't be blank!")]
        [EmailAddress(ErrorMessage = "Email should be in proper email format!")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password can't be blank!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
