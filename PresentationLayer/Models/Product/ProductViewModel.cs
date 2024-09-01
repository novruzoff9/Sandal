namespace PresentationLayer.Models.Product;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = null!;
    public bool IsInWishlist { get; set; }
}

