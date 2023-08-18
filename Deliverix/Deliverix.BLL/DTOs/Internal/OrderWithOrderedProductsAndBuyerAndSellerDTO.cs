using Deliverix.Common.Enums;

namespace Deliverix.BLL.DTOs.Internal;

public class OrderWithOrderedProductsAndBuyerAndSellerDTO
{
    public int Id { get; set; }

    public int BuyerId { get; set; }
    public int? SellerId { get; set; }
    public string DeliveryAddress { get; set; }
    public string Comment { get; set; }
    public decimal FullPrice { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    public DateTime? DeliveryDateTime { set; get; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public IEnumerable<OrderedProductDTO> OrderedProducts { get; set; }
    public UserDTO Buyer { get; set; }
    public UserDTO? Seller { get; set; }
}