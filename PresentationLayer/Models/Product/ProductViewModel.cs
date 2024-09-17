using EntityLayer.Concrete;

namespace PresentationLayer.Models.Product;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public bool IsInWishlist { get; set; }
}

public class ShopViewModel
{
    public List<ProductViewModel> Products { get; set; }
    public List<Category> Categories { get; set; }
}