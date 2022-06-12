
namespace Deliverix.DAL.Models;

public class OrderedProduct
{
    
    public int Id { get; set; }
    
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public Order Order { get; set; }
    public Product Product { get; set; }
}