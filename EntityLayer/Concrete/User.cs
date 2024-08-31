using EntityLayer.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete;

public class User : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    [DataType(DataType.Date)]
    public DateTime RegistrationDate { get; set; }
    public DateOnly? BirthDay { get; set; }
    public string? Gender { get; set; }
    public ICollection<UserFavoriteProduct>? FavoriteProducts { get; set; }
}
