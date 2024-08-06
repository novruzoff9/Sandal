using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete;

public class Product
{
    public int ProductID { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Rating { get; set; }

}
