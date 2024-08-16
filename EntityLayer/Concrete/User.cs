using EntityLayer.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete;

public class User : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public bool IsVendor { get; set; } = false;
    [DataType(DataType.Date)]
    public DateTime RegistrationDate { get; set; }
}
