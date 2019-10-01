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
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}