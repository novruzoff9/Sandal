using Microsoft.AspNet.Identity.EntityFramework;

namespace EntityLayer.Concrete;

public class Role : IdentityRole
{
    public DateTime? ExpireDate { get; set; }
}
