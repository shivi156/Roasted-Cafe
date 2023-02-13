using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using roasted1.Data;
using System.Linq;
using roasted1.Models;


namespace roasted1.Pages.Checkout;

[Authorize]

public class Checkout : PageModel
    
{
    public OrderHistory Order = new OrderHistory();
    private readonly roasted1Context _db;
    private readonly UserManager<ApplicationUser> _UserManager;
    public IList<CheckoutItems> Items { get; private set; }
    // public decimal Total = 0;

    public Checkout(roasted1Context db, 
        UserManager<ApplicationUser> UserManager)
    {
        _db = db;
        _UserManager = UserManager;
    }

    public async Task OnGetAsync()
    {
        
        var user = await _UserManager.GetUserAsync(User);
        CheckoutCustomer customer = await _db
            .CheckoutCustomers
            .FindAsync(user.Email);
        
    
        
        Items = _db.CheckoutItems.FromSqlRaw(
            "SELECT tbl_menu.Id, tbl_menu.Price, " +
            "tbl_menu.ItemName, " +
            "BasketItems.BasketID, BasketItems.Quantity " +
            "FROM tbl_menu INNER JOIN BasketItems " +
            "ON tbl_menu.Id = BasketItems.StockID " +
            "WHERE BasketID = {0}", customer.BasketID
        ).ToList();

    }
    
    
    
    public async Task<IActionResult> OnPostBuyAsync()
    {
        var currentOrder = _db.OrderHistories
            .FromSqlRaw("SELECT OrderNo, Email From OrderHistories")
            .OrderByDescending(b => b.OrderNo)
            .FirstOrDefault();

        if (currentOrder == null)
        {
            Order.OrderNo = 1;
        }
        else
        {
            Order.OrderNo = currentOrder.OrderNo + 1;
        }

        var user = await _UserManager.GetUserAsync(User);
        Order.Email = user.Email;
        _db.OrderHistories.Add(Order);

        CheckoutCustomer customer = await _db
            .CheckoutCustomers
            .FindAsync(user.Email);

        var basketItems = 
            _db.BasketItems
                .FromSqlRaw("SELECT StockID, BasketID, Quantity From BasketItems " +
                            "WHERE BasketID = {0}", customer.BasketID)
                .ToList();

        foreach(var item in basketItems) 
        {
            OrderItem oi = new OrderItem
            {
                OrderNo = Order.OrderNo,
                StockID = item.StockID,
                Quantity = item.Quantity
            };
            _db.OrderItems.Add(oi);
            _db.BasketItems.Remove(item);
        }

        await _db.SaveChangesAsync();

        return RedirectToPage("/Checkout/ThankYou");
    }
    
    public async Task<IActionResult> OnPostIncreaseAsync(int id)
    {
        var user = await _UserManager.GetUserAsync(User);
        CheckoutCustomer customer = await _db
            .CheckoutCustomers
            .FindAsync(user.Email);

        var basketItem = 
            _db.BasketItems
                .FromSqlRaw("SELECT StockID, BasketID, Quantity From BasketItems " +
                            "WHERE BasketID = {0} and StockID = {1}", customer.BasketID, id)
                .FirstOrDefault();

        if (basketItem == null)
        {
            return NotFound();
        }

        basketItem.Quantity++;
        _db.BasketItems.Update(basketItem);
        await _db.SaveChangesAsync();

        return RedirectToPage();
    }

    

    
 

    
    

}

    
 
     



        
        
    