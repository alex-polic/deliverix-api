using Deliverix.BLL.DTOs;
using Deliverix.BLL.DTOs.Internal;
using Deliverix.BLL.DTOs.Requests;

namespace Deliverix.BLL.Contracts;

public interface IOrderService
{
    public Task<OrderDTO> GetById(int id);
    public Task<OrderDTO> GetByIdWithOrderedProducts(int id);
    public Task<OrderWithOrderedProductsDTO> GetCurrentForBuyerWithOrderedProducts(int buyerId);
    public Task<OrderWithOrderedProductsDTO> GetCurrentForCourierWithOrderedProducts(int courierId);
    public Task<IEnumerable<OrderWithOrderedProductsDTO?>> GetAllWithOrderedProducts();
    public Task<IEnumerable<OrderDTO?>> GetAll();
    public Task<OrderWithOrderedProductsDTO> CreateWithOrderedProducts(OrderCreateDTO order);
    public Task<OrderDTO> Create(OrderDTO order);
    public Task<OrderDTO> Update(OrderDTO order);
    public Task<OrderDTO> Delete(int id);
}