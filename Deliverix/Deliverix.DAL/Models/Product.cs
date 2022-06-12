namespace Deliverix.DAL.Models;

public class Product
{
    public Product()
    {
        OrderedProducts = new HashSet<OrderedProduct>();
    }
    
    public int Id { get; set; }
    
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string IngredientsDescription { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<OrderedProduct> OrderedProducts { get; set; }
}