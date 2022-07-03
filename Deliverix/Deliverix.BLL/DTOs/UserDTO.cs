using Deliverix.Common.Enums;

namespace Deliverix.BLL.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public UserType UserType { get; set; }
    public string ProfilePictureUrl { get; set; }
    public VerificationStatus VerificationStatus { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}