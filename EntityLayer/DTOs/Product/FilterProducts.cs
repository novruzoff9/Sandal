using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.Product;

public class FilterProducts
{
    public string? CategoryName { get; set; }
    public List<Color>? Colors { get; set; } = null;
    public decimal MinPrice { get; set; } = 0;
    public decimal MaxPrice { get; set; } = 0;
    public List<int>? Ratings { get; set; } = null;
}
