using Microsoft.Build.Framework;

namespace roasted1.Data;

public class BasketItem
{
    [Required]
    public int StockID { get; set; }
    [Required]
    public int BasketID { get; set; }
    [Required]
    public int Quantity { get; set; }

}