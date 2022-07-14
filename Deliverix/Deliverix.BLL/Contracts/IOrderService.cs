using Deliverix.BLL.DTOs;
using Deliverix.BLL.DTOs.Internal;
using Deliverix.BLL.DTOs.Requests;

namespace Deliverix.BLL.Contracts;

public interface IOrderService
{
    public Task<OrderDTO> GetById(int id);
    public Task<OrderDTO> GetByIdWithOrderedProducts(int id);
    public Task<OrderWithOrderedProductsAndBuyerAndCourierDTO?> GetCurrentForBuyerWithOrderedProducts(int buyerId);
    public Task<OrderWithOrderedProductsAndBuyerAndCourierDTO> GetCurrentForCourierWithOrderedProducts(int courierId);
    public Task<IEnumerable<OrderWithOrderedProductsAndBuyerAndCourierDTO?>> GetAllWithOrderedProducts();
    public Task<IEnumerable<OrderWithOrderedProductsAndBuyerAndCourierDTO?>> GetAllPastForBuyer(int buyerId);
    public Task<IEnumerable<OrderWithOrderedProductsAndBuyerAndCourierDTO?>> GetAllPastForCourier(int courierId);
    public Task<IEnumerable<OrderWithOrderedProductsAndBuyerAndCourierDTO?>> GetAllPendingOrders();
    public Task<OrderWithOrderedProductsAndBuyerAndCourierDTO?> AcceptDeliveryOfOrder(int orderId, int courierId);
    public Task<OrderWithOrderedProductsAndBuyerAndCourierDTO?> FinishDeliveryOfOrder(int orderId);
    public Task<IEnumerable<OrderDTO?>> GetAll();
    public Task<OrderWithOrderedProductsAndBuyerAndCourierDTO> CreateWithOrderedProducts(OrderCreateDTO order);
    public Task<OrderDTO> Create(OrderDTO order);
    public Task<OrderDTO> Update(OrderDTO order);
    public Task<OrderDTO> Delete(int id);
}