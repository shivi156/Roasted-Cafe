using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using roasted1.Data;

namespace roasted1.Pages.Account

{
    public class LoginModel : PageModel
    {
        [BindProperty]
            public LoginUser Input { get; set; }
            

            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly ILogger<LoginModel> _logger;

            public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger) 
            {
                
                _signInManager = signInManager;
                _logger = logger;
            }

           
            public async Task<IActionResult> OnPostAsync()
            {
               

                if (ModelState.IsValid)
                {

                    var user = await _signInManager.UserManager.FindByEmailAsync(Input.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
                        return Page();
                    }
                    var result = await _signInManager.CheckPasswordSignInAsync(user, Input.Password, false); 
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, true);
                        _logger.LogInformation("User logged in.");
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                    }
                }

                // If we got this far, something failed, redisplay form
                return Page();
            }

        }
    }

