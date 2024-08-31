using EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Rating { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public ICollection<UserFavoriteProduct>? UserFavorites { get; set; }
}
