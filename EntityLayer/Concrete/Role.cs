using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete;

public class Role : IdentityRole
{
    public DateTime? ExpireDate { get; set; }
}
