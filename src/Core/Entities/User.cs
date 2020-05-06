using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
	public class User : IdentityUser
	{
		public ICollection<Order> Orders { get; set; }
	}

	//public class IdentityUserToken : IdentityUserToken<string>
	//{
	//	public 
	//}
}
