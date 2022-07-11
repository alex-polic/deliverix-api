using Deliverix.Common.Enums;

namespace Deliverix.BLL.DTOs.Internal;

public class OrderWithOrderedProductsDTO
{
    public int Id { get; set; }

    public int BuyerId { get; set; }
    public int? CourierId { get; set; }
    public string DeliveryAddress { get; set; }
    public string Comment { get; set; }
    public decimal FullPrice { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public IEnumerable<OrderedProductDTO> OrderedProducts { get; set; }
}