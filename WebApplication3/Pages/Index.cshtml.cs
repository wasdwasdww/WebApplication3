using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApplication3.Model;
using Microsoft.AspNetCore.DataProtection;

namespace WebApplication3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public readonly IDataProtectionProvider _dataProtectionProvider;

        public IndexModel(ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IDataProtectionProvider dataProtectionProvider)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _dataProtectionProvider = dataProtectionProvider;
        }

        public ApplicationUser CurrentUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_signInManager.IsSignedIn(User))
            {
                // Retrieve current user's information
                CurrentUser = await _userManager.GetUserAsync(User);
                return Page();
            }
            else
            {
                return RedirectToPage("Login");
            }
        }
    }
}
