using System.ComponentModel.DataAnnotations;
namespace TradingPlatform.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; } = default!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
