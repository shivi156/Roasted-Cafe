using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using roasted1.Data;
using roasted1.Models;

namespace roasted1.Pages.Menus
{
    [Authorize(Roles = "Admin")]

    public class CreateModel : PageModel
    {
        private readonly roasted1.Data.roasted1Context _context;

        public CreateModel(roasted1.Data.roasted1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Menu Menu { get; set; } = default!;
        

        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Menu == null || Menu == null)
          {
                return Page();
          }
          
          foreach (var file in Request.Form.Files)
          {
              MemoryStream ms = new MemoryStream();
              file.CopyTo(ms);
              Menu.ImageData = ms.ToArray();
              ms.Close();
              ms.Dispose();
          }

          _context.Menu.Add(Menu);
          await _context.SaveChangesAsync();
          return RedirectToPage("/Menus/Index");
          
        }
    }
}
