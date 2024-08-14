using EntityLayer.Base;
using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete;

public class FAQ : BaseEntity
{
    public FaqCategory Category { get; set; }
    public string Question { get; set; } = null!;
    public string Answer { get; set; } = null!;
}
