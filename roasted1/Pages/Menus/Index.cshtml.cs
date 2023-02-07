using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using roasted1.Data;
using roasted1.Models;


namespace roasted1.Pages.Menus
{
    
    // [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        
        
        
        private readonly roasted1.Data.roasted1Context _context;

        public IndexModel(roasted1.Data.roasted1Context context)
        {
            _context = context;
        }

        public IList<Menu> Menu { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Menu != null)
            {
                Menu = await _context.Menu.ToListAsync();
            }
        }
    }
}
