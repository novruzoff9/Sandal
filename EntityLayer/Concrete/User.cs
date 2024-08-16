using EntityLayer.Base;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete;

//public class User : IdentityUser
public class User : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsVendor { get; set; } = false;
    [DataType(DataType.Date)]
    public DateTime RegistrationDate { get; set; }
}
