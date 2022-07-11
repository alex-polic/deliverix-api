using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.DTOs.Internal;
using Deliverix.BLL.DTOs.Requests;
using Deliverix.BLL.Mappers;
using Deliverix.Common.Enums;
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

    private readonly IUserService _userService;
    private readonly IOrderedProductService _orderedProductService;
    public OrderService()
    {
        _context = new UnitOfWork();
        _orderRepository = new OrderRepository(_context);

        _userService = new UserService();
        _orderedProductService = new OrderedProductService();
    }
    public async Task<OrderDTO> GetById(int id)
    {
        var order = await _orderRepository.GetById(id);
        
        if (order == null)
            throw new BusinessException("Order with given ID not found", 400);
        
        return ObjectMapper.Mapper.Map<OrderDTO>(order);
    }
    
    public async Task<OrderDTO> GetByIdWithOrderedProducts(int id)
    {
        var order = await _orderRepository.GetByIdWithOrderedProducts(id);
        
        if (order == null)
            throw new BusinessException("Order with given ID not found", 400);
        
        return ObjectMapper.Mapper.Map<OrderDTO>(order);
    }

    public async Task<OrderWithOrderedProductsDTO?> GetCurrentForBuyerWithOrderedProducts(int buyerId)
    {
        var order = await _orderRepository.GetCurrentForBuyerWithOrderedProducts(buyerId);
        
        return ObjectMapper.Mapper.Map<OrderWithOrderedProductsDTO>(order);
    }

    public async Task<OrderWithOrderedProductsDTO> GetCurrentForCourierWithOrderedProducts(int courierId)
    {
        var order = await _orderRepository.GetCurrentForCourierWithOrderedProducts(courierId);
        
        if (order == null)
            throw new BusinessException("Courier with given ID does not have pending Order", 400);
        
        return ObjectMapper.Mapper.Map<OrderWithOrderedProductsDTO>(order);
    }

    public async Task<IEnumerable<OrderWithOrderedProductsDTO?>> GetAllWithOrderedProducts()
    {
        var orders = await _orderRepository.GetAllWithOrderedProducts();
        
        return ObjectMapper.Mapper.Map<IEnumerable<OrderWithOrderedProductsDTO>>(orders);
    }

    public async Task<IEnumerable<OrderWithOrderedProductsDTO?>> GetAllPastForBuyer(int buyerId)
    {
        var orders = await _orderRepository.GetAllPastForBuyer(buyerId);

        return ObjectMapper.Mapper.Map<IEnumerable<OrderWithOrderedProductsDTO>>(orders);
    }

    public async Task<IEnumerable<OrderDTO?>> GetAll()
    {
        var orders = await _orderRepository.GetAll();

        return ObjectMapper.Mapper.Map<IEnumerable<OrderDTO>>(orders);
    }

    public async Task<OrderWithOrderedProductsDTO> CreateWithOrderedProducts(OrderCreateDTO order)
    {
        if (order.BuyerId <= 0) throw new BusinessException("Buyer ID is mandatory field", 400);
        if(order.OrderedProducts.Count() <= 0) 
            throw new BusinessException("Order must have at least one product", 400);
        if (order.OrderedProducts.Any(e => e.Amount <= 0))
            throw new BusinessException("All products must have amount of at least 1", 400);
        

        await _userService.GetById(order.BuyerId);
        try
        {
            await GetCurrentForBuyerWithOrderedProducts(order.BuyerId);
            throw new BusinessException("Buyer cannot make new Order while they have a pending Order", 400);
        }
        catch (BusinessException e)
        {
            
        }

        OrderDTO newOrder = await Create(new OrderDTO()
        {
            BuyerId = order.BuyerId,
            Comment = order.Comment,
            DeliveryAddress = order.DeliveryAddress,
            DeliveryStatus = DeliveryStatus.Pending
        });

        foreach (var orderedProduct in order.OrderedProducts)
        {
            await _orderedProductService.Create(new OrderedProductDTO()
            {
                OrderId = newOrder.Id,
                ProductId = orderedProduct.ProductId,
                Amount = orderedProduct.Amount
            });
        }

        var orderToReturn = await GetByIdWithOrderedProducts(newOrder.Id);
        
        return ObjectMapper.Mapper.Map<OrderWithOrderedProductsDTO>(orderToReturn);
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