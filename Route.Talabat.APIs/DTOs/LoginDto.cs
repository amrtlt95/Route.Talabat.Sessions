﻿using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.DTOs
{
	public class LoginDto
	{
		[Required,EmailAddress]
        public string Email { get; set; } = null!;

		[Required]
		public string Password { get; set; } = null!;
    }
}
