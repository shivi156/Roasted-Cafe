using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using roasted1;
using roasted1.Data;

namespace roasted1.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty] public RegistrationModel Input { get; set; }

        private roasted1Context _db;
        public CheckoutCustomer Customer = new CheckoutCustomer();
        public Basket Basket = new Basket();


        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            roasted1Context db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    // await _userManager.AddToRoleAsync(user, "Member");
                    NewBasket();
                    NewCustomer(Input.Email);
                    await _db.SaveChangesAsync();

                    return RedirectToPage("/Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }



        private void NewBasket()
        {
            var currentBasket = _db.Baskets.FromSqlRaw("SELECT * From Baskets")
                .OrderByDescending(b => b.BasketID)
                .FirstOrDefault();
            if (currentBasket == null)
            {
                Basket.BasketID = 1;
            }
            else
            {
                Basket.BasketID = currentBasket.BasketID + 1;
            }

            _db.Baskets.Add(Basket);


        }

        private void NewCustomer(string Email)
        {
            Customer.Email = Email;
            Customer.BasketID = Basket.BasketID;
            _db.CheckoutCustomers.Add(Customer);

        }

    }
}
