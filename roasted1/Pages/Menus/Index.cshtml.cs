using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using roasted1.Data;
using roasted1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using roasted1.Pages.Account;


namespace roasted1.Pages.Menus
{
    
    
    public class IndexModel : PageModel
    {
        
        private readonly roasted1.Data.roasted1Context _context;
        private static UserManager<ApplicationUser> _userManager ;

        public IndexModel(roasted1.Data.roasted1Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Menu> Menu { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Menu != null)
            {
                Menu = await _context.Menu.ToListAsync();
            }
        }
        
     

        public async Task<IActionResult> OnPostBuyAsync(int itemID)
        {
            var user = await _userManager.GetUserAsync(User);
            CheckoutCustomer customer = await _context
                .CheckoutCustomers
                .FindAsync(user.Email);

            var item = _context.BasketItems.FromSqlRaw("SELECT StockID, BasketID, Quantity FROM BasketItems WHERE StockID = {0} AND BasketID = {1}", itemID, customer.BasketID)
                .ToList()
                .FirstOrDefault();

            if (item == null)
            {
                BasketItem newItem = new BasketItem
                {
                    BasketID = customer.BasketID,
                    StockID = itemID,
                    Quantity = 1
                };
                _context.BasketItems.Add(newItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                item.Quantity = item.Quantity + 1;
                _context.Attach(item).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw new Exception($"Basket not found!", e);
                }
            }
            return RedirectToPage();
            
        }
        
        }
        
        
    }

