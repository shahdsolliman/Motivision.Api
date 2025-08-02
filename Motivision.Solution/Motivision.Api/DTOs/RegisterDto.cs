using System.ComponentModel.DataAnnotations;

namespace Motivision.API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        //[RegularExpression("?",
        //    ErrorMessage = "")]
        public string Password { get; set; }
    }
}
