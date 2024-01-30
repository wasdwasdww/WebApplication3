using Microsoft.AspNetCore.Identity;
using WebApplication3.Model;
using WebApplication3.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text.Encodings.Web;

namespace WebApplication3.Pages
{
	public class RegisterModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly HtmlEncoder _encoder;

        [BindProperty]
		public Register RModel { get; set; }
		public string PasswordStrengthStatus { get; set; }
		public Color PasswordStrengthColor { get; set; }

		public RegisterModel(UserManager<ApplicationUser> userManager,
							  SignInManager<ApplicationUser> signInManager,
							  IDataProtectionProvider dataProtectionProvider)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_dataProtectionProvider = dataProtectionProvider;
			_encoder = HtmlEncoder.Default;

        }

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				// Hash the password
				var passwordHasher = new PasswordHasher<ApplicationUser>();
				var hashedPassword = passwordHasher.HashPassword(null, RModel.Password);

				// Create data protector with a purpose string
				var protector = _dataProtectionProvider.CreateProtector("MySecretKey");

				var user = new ApplicationUser()
				{
                    FirstName = _encoder.Encode(RModel.FirstName),
                    LastName = _encoder.Encode(RModel.LastName),
                    CreditCard = protector.Protect(_encoder.Encode(RModel.CreditCardNo)),
                    MobileNo = _encoder.Encode(RModel.MobileNo),
                    BillingAddress = _encoder.Encode(RModel.BillingAddress),
                    ShippingAddress = _encoder.Encode(RModel.ShippingAddress),
                    Email = _encoder.Encode(RModel.Email),
                    Password = hashedPassword
                };

				user.UserName = Guid.NewGuid().ToString();

				var result = await _userManager.CreateAsync(user);
				if (result.Succeeded)
				{
					// Store user's email in session
					HttpContext.Session.SetString("UserEmail", user.Email);

					await _signInManager.SignInAsync(user, false);
					return RedirectToPage("Index");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return Page();
		}
	}
}
