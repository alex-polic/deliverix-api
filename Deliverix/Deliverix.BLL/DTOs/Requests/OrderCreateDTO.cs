namespace Deliverix.BLL.DTOs.Requests;

public class OrderCreateDTO
{
    public int BuyerId { get; set; }
    public string DeliveryAddress { get; set; }
    public string Comment { get; set; }

    public IEnumerable<OrderedProductCreateDTO> OrderedProducts { get; set; }
    
}