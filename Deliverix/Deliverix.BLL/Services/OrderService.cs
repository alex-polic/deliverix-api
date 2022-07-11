using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.DTOs.Internal;
using Deliverix.BLL.DTOs.Requests;
using Deliverix.BLL.Mappers;
using Deliverix.Common.Exceptions;
using Deliverix.DAL;
using Deliverix.DAL.Contracts;
using Deliverix.DAL.Models;
using Deliverix.DAL.Repositories;

namespace Deliverix.BLL.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly UnitOfWork _context;
    public OrderService()
    {
        _context = new UnitOfWork();
        _orderRepository = new OrderRepository(_context);
    }
    public async Task<OrderDTO> GetById(int id)
    {
        var order = await _orderRepository.GetById(id);
        
        if (order == null)
            throw new BusinessException("Order with given ID not found", 400);
        
        return ObjectMapper.Mapper.Map<OrderDTO>(order);
    }

    public async Task<OrderWithOrderedProductsDTO> GetCurrentForBuyerWithOrderedProducts(int buyerId)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderWithOrderedProductsDTO> GetCurrentForCourierWithOrderedProducts(int courierId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<OrderWithOrderedProductsDTO?>> GetAllWithOrderedProducts()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<OrderDTO?>> GetAll()
    {
        var orders = await _orderRepository.GetAll();

        return ObjectMapper.Mapper.Map<IEnumerable<OrderDTO>>(orders);
    }

    public async Task<OrderWithOrderedProductsDTO> CreateWithOrderedProducts(OrderCreateDTO order)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderDTO> Create(OrderDTO order)
    {
        Order model = ObjectMapper.Mapper.Map<Order>(order);
        
        Order result = await _orderRepository.Create(model);

        await _context.SaveChangesAsync();
        
        return ObjectMapper.Mapper.Map<OrderDTO>(result);
    }

    public async Task<OrderDTO> Update(OrderDTO order)
    {
        OrderDTO orderFound = await GetById(order.Id);
        
        if (orderFound == null)
            throw new BusinessException("Order with given ID does not exist", 400);

        orderFound.BuyerId = order.BuyerId;
        orderFound.CourierId = order.CourierId;
        orderFound.DeliveryAddress = order.DeliveryAddress;
        orderFound.Comment = order.Comment;
        orderFound.FullPrice = order.FullPrice;
        orderFound.DeliveryStatus = order.DeliveryStatus;
        
        Order model = ObjectMapper.Mapper.Map<Order>(orderFound);
        
        Order result = await _orderRepository.Update(model);

        await _context.SaveChangesAsync();
        
        return ObjectMapper.Mapper.Map<OrderDTO>(result);
    }

    public async Task<OrderDTO> Delete(int id)
    {
        var order = await _orderRepository.Delete(id);
        
        await _context.SaveChangesAsync();
        
        return ObjectMapper.Mapper.Map<OrderDTO>(order);
    }
}