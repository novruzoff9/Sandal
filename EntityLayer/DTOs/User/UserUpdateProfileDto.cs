namespace EntityLayer.DTOs.User;

public class UserUpdateProfileDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public DateOnly? BirthDay { get; set; }
    public string? Gender { get; set; }
}
