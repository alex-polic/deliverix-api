using Deliverix.Common.Enums;

namespace Deliverix.DAL.Models;

public class User
{
    public User()
    {
        BuyerOrders = new HashSet<Order>();
        CourierOrders = new HashSet<Order>();
    }

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
    
    public ICollection<Order> BuyerOrders { get; set; }
    public ICollection<Order> CourierOrders { get; set; }
}