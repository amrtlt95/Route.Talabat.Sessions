using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.DTOs
{
	public class RegisterDto
	{
        [Required]
		public string DisplayName { get; set; } = null!;
		[Required,EmailAddress]
		public string Email { get; set; } = null !;
		[Required,Phone]
		public string PhoneNumber { get; set; } = null!;
		[Required]
		public string Password { get; set; } = null!;
    }
}
