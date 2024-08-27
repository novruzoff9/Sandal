using System.Drawing;

namespace PresentationLayer.Models;

public class FilterProducts
{
    public string? CategoryName { get; set; }
    public Color? Color { get; set; }
    public decimal MinPrice { get; set; } = 0;
    public decimal MaxPrice { get; set; } = 0;
    public decimal MinRating { get; set; } = 0;
    public decimal MaxRating { get; set; } = 5;
}
