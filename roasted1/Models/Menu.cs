namespace roasted1.Models;

public class Menu
{
    public int Id { get; set; }
    public string ItemName { get; set; }
    public decimal Price { get; set; }
   
    public string ImageDescription { get; set; }
    public byte[] ImageData { get; set; }
   
}