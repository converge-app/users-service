using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTransferObjects
{
    public class UserRegistrationDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "First name has to be at least 2 characters long")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name has to be at least 2 characters long")]
        public string LastName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Username has to be at least 2 characters long")]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password has to be at least 8 characters")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}