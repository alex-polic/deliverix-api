using Deliverix.DAL.Models;

namespace Deliverix.DAL.Contracts;

public interface IOrderedProductRepository
{
    public Task<OrderedProduct?> GetById(int id);
    public Task<IEnumerable<OrderedProduct?>> GetAll();
    public Task<OrderedProduct> Create(OrderedProduct orderedProduct);
    public Task<OrderedProduct> Update(OrderedProduct orderedProduct);
    public Task<OrderedProduct> Delete(int id);
}