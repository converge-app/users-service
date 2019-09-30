using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTransferObjects
{
    public class UserCreationDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "First name has to be at least 2 characters long")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name has to be at least 2 characters long")]
        public string LastName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Email has to be at least 2 characters long")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}