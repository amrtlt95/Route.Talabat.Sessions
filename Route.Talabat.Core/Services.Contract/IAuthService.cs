﻿using Route.Talabat.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Services.Contract
{
	public interface IAuthService
	{
		Task<string> CreateTokenAsync(ApplicationUser user);
	}
}
