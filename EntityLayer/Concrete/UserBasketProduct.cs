namespace EntityLayer.Concrete;

public class UserBasketProduct
{
    public string UserId { get; set; }
    public User User { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}