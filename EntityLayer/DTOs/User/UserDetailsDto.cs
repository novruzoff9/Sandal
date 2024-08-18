using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.User;

public class UserDetailsDto
{
    public string UserName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Roles { get; set; }
    [DataType(DataType.Date)]
    public DateTime RegistrationDate { get; set; }
    public string? NormalizedUserName { get; set; }
    [ProtectedPersonalData]
    public string? Email { get; set; }
    public string? NormalizedEmail { get; set; }
    [PersonalData]
    public bool EmailConfirmed { get; set; }
    public string? SecurityStamp { get; set; }
    public string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    [ProtectedPersonalData]
    public string? PhoneNumber { get; set; }
    [PersonalData]
    public bool PhoneNumberConfirmed { get; set; }
    [PersonalData]
    public bool TwoFactorEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
}
