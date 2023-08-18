using Deliverix.BLL.DTOs;
using Deliverix.BLL.DTOs.Internal;
using Deliverix.BLL.DTOs.Requests;

namespace Deliverix.BLL.Contracts;

public interface IOrderService
{
    public Task<OrderDTO> GetById(int id);
    public Task<OrderDTO> GetByIdWithOrderedProducts(int id);
    public Task<OrderWithOrderedProductsAndBuyerAndSellerDTO?> GetCurrentForBuyerWithOrderedProducts(int buyerId);
    public Task<OrderWithOrderedProductsAndBuyerAndSellerDTO> GetCurrentForSellerWithOrderedProducts(int sellerId);
    public Task<IEnumerable<OrderWithOrderedProductsAndBuyerAndSellerDTO?>> GetAllWithOrderedProducts();
    public Task<IEnumerable<OrderWithOrderedProductsAndBuyerAndSellerDTO?>> GetAllPastForBuyer(int buyerId);
    public Task<IEnumerable<OrderWithOrderedProductsAndBuyerAndSellerDTO?>> GetAllPastForSeller(int sellÍerId);
    public Task<IEnumerable<OrderWithOrderedProductsAndBuyerAndSellerDTO?>> GetAllPendingOrders();
    public Task<OrderWithOrderedProductsAndBuyerAndSellerDTO?> AcceptDeliveryOfOrder(int orderId, int sellerId);
    public Task<OrderWithOrderedProductsAndBuyerAndSellerDTO?> FinishDeliveryOfOrder(int orderId);
    public Task<IEnumerable<OrderDTO?>> GetAll();
    public Task<OrderWithOrderedProductsAndBuyerAndSellerDTO> CreateWithOrderedProducts(OrderCreateDTO order);
    public Task<OrderDTO> Create(OrderDTO order);
    public Task<OrderDTO> Update(OrderDTO order);
    public Task<OrderDTO> Delete(int id);
}