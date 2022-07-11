namespace Deliverix.BLL.DTOs;

public class OrderedProductDTO
{
    public int Id { get; set; }
    
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}