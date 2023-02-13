using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace roasted1.Pages.Admin;
[Authorize]
public class Admin : PageModel

{
    
    public void OnGet()
    {
        
    }
}