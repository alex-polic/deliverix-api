using Deliverix.Common.Enums;

namespace Deliverix.DAL.Models;

public class Order
{
    public Order()
    {
        OrderedProducts = new HashSet<OrderedProduct>();
    }
    
    public int Id { get; set; }

    public int BuyerId { get; set; }
    public int? SellerId { get; set; }
    public string DeliveryAddress { get; set; }
    public string Comment { get; set; }
    public decimal FullPrice { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }

    public DateTime? DeliveryDateTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public User Buyer { get; set; }
    public User? Seller { get; set; }
    
    public ICollection<OrderedProduct> OrderedProducts { get; set; }
}