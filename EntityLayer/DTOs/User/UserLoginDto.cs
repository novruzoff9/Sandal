using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.User;

public class UserLoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}
