using Deliverix.DAL.Models;

namespace Deliverix.DAL.Contracts;

public interface IOrderRepository
{
    public Task<Order?> GetById(int id);
    public Task<Order?> GetByIdWithOrderedProducts(int id);
    public Task<Order?> GetCurrentForBuyerWithOrderedProducts(int buyerId);
    public Task<Order?> GetCurrentForCourierWithOrderedProducts(int courierId);
    public Task<IEnumerable<Order?>> GetAll();
    public Task<IEnumerable<Order?>> GetAllWithOrderedProducts();
    public Task<Order> Create(Order order);
    public Task<Order> Update(Order order);
    public Task<Order> Delete(int id);
}