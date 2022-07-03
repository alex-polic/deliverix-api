using Deliverix.Common.Enums;

namespace Deliverix.BLL.DTOs.Internal;

public class UserRegisterDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; }
    public UserType UserType { get; set; }
    public string ProfilePicturePath { get; set; }
}