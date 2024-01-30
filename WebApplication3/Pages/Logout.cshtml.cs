using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;

namespace WebApplication3.Pages
{
	public class LogoutModel : PageModel
	{
		private readonly SignInManager<ApplicationUser> _signInManager;

		public LogoutModel(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}

		public void OnGet() { }

		public async Task<IActionResult> OnPostLogoutAsync()
		{
			await _signInManager.SignOutAsync();
			return RedirectToPage("/Index"); // Adjust the redirection path as needed
		}

		public async Task<IActionResult> OnPostDontLogoutAsync()
		{
			return RedirectToPage("/Index"); // Adjust the redirection path as needed
		}
	}
}
