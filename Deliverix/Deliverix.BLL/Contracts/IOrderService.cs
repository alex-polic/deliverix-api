using Deliverix.BLL.DTOs;

namespace Deliverix.BLL.Contracts;

public interface IOrderService
{
    public Task<OrderDTO> GetById(int id);
    public Task<IEnumerable<OrderDTO?>> GetAll();
    public Task<OrderDTO> Create(OrderDTO order);
    public Task<OrderDTO> Update(OrderDTO order);
    public Task<OrderDTO> Delete(int id);
}