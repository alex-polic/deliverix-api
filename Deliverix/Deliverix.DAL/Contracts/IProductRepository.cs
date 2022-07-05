using Deliverix.DAL.Models;

namespace Deliverix.DAL.Contracts;

public interface IProductRepository
{
    public Task<Product?> GetById(int id);
    public Task<IEnumerable<Product?>> GetAll();
    public Task<Product> Create(Product product);
    public Task<Product> Update(Product product);
    public Task<Product> Delete(int id);
}