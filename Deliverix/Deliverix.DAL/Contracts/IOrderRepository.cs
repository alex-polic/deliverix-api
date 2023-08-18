using Deliverix.DAL.Models;

namespace Deliverix.DAL.Contracts;

public interface IOrderRepository
{
    public Task<Order?> GetById(int id);
    public Task<Order?> GetByIdWithOrderedProducts(int id);
    public Task<Order?> GetCurrentForBuyerWithOrderedProducts(int buyerId);
    public Task<Order?> GetCurrentForSellerWithOrderedProducts(int sellerId);
    public Task<IEnumerable<Order?>> GetAll();
    public Task<IEnumerable<Order?>> GetAllWithOrderedProducts();
    public Task<IEnumerable<Order?>> GetAllPastForBuyer(int buyerId);
    public Task<IEnumerable<Order?>> GetAllPastForSeller(int sellerId);
    public Task<IEnumerable<Order?>> GetAllPendingOrders();
    public Task<Order> Create(Order order);
    public Task<Order> Update(Order order);
    public Task<Order> Delete(int id);
}