﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.User;

public class UserRegisterDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? UserName { get; set; }
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public bool AcceptPrivacy { get; set; }
    public bool AllowAds { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
}
