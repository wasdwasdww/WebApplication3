using Microsoft.AspNetCore.Identity;

namespace WebApplication3.Model
{
	public class ApplicationUser : IdentityUser
	{

		// Additional properties for user registration
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string CreditCard {  get; set; }
		public string MobileNo { get; set; }
		public string BillingAddress { get; set; }
		public string ShippingAddress { get; set; }
		public string Email { get; set; }
		public string Password {  get; set; }


	}
}

