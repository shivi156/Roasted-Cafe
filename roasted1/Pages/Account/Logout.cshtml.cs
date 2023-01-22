using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using roasted1.Data;

namespace roasted1.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPost()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}