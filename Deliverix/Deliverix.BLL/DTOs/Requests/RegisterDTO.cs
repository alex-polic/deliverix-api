using Deliverix.Common.Enums;

namespace Deliverix.BLL.DTOs.Requests;

public class RegisterDTO
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public UserType UserType { get; set; }
    public string ProfilePictureUrl { get; set; }
}