using Deliverix.DAL.Models;

namespace Deliverix.DAL.Contracts;

public interface IOrderRepository
{
    public Task<Order?> GetById(int id);
    public Task<IEnumerable<Order?>> GetAll();
    public Task<Order> Create(Order order);
    public Task<Order> Update(Order order);
    public Task<Order> Delete(int id);
}