using EntityLayer.DTOs.User;

namespace PresentationLayer.Models;

public class AccountViewModel
{
    public UserLoginDto? Login { get; set; }
    public UserRegisterDto? Register { get; set; }
    public bool IsRegistering { get; set; }
}
