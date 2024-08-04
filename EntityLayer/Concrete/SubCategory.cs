﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete;

public class SubCategory
{
    public int SubCategoryId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public virtual Category? Category { get; set; }
}
