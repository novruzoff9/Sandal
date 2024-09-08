using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.Category;

public class CategoryViewDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? ParentCategoryName { get; set; }
    public ICollection<CategoryViewDto>? SubCategories { get; set; }
    //public ICollection<Product>? Products { get; set; }
}
