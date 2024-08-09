using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category>? SubCategories { get; set; }
    public ICollection<Product>? Products { get; set; }
}
