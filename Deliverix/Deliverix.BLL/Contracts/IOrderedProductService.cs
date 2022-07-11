using Deliverix.BLL.DTOs;

namespace Deliverix.BLL.Contracts;

public interface IOrderedProductService
{
    public Task<OrderedProductDTO> GetById(int id);
    public Task<IEnumerable<OrderedProductDTO?>> GetAll();
    public Task<OrderedProductDTO> Create(OrderedProductDTO orderedOrderedProduct);
    public Task<OrderedProductDTO> Update(OrderedProductDTO orderedOrderedProduct);
    public Task<OrderedProductDTO> Delete(int id);
}