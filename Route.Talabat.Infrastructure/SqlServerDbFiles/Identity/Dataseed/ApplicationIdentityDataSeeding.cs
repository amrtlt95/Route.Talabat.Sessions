using Microsoft.AspNetCore.Identity;
using Route.Talabat.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.SqlServerDbFiles.Identity.Dataseed
{
	public class ApplicationIdentityDataSeeding
	{
		public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
		{
			if(!userManager.Users.Any())
			{
				var user = new ApplicationUser()
				{
					DisplayName = "Amr Talaat",
					Email = "amrtlt90@gmail.com",
					PhoneNumber = "01273885940",
					UserName = "amrtlt90"
				};
				await userManager.CreateAsync(user, "123456789@Aab");
			}
		}
	}
}
