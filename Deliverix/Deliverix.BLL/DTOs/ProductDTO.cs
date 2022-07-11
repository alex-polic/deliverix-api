namespace Deliverix.BLL.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string IngredientsDescription { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}