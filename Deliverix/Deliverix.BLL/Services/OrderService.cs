using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.DTOs.Internal;
using Deliverix.BLL.DTOs.Requests;
using Deliverix.BLL.Mappers;
using Deliverix.Common.Configurations;
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
    private readonly IProductService _productService;

    private readonly int DELIVERY_CHARGE;
    public OrderService()
    {
        _context = new UnitOfWork();
        _orderRepository = new OrderRepository(_context);

        _userService = new UserService();
        _orderedProductService = new OrderedProductService();
        _productService = new ProductService();

        DELIVERY_CHARGE = int.Parse(AppConfiguration.GetConfiguration("DeliveryCharge"));
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

    public async Task<OrderWithOrderedProductsAndBuyerAndSellerDTO?> GetCurrentForBuyerWithOrderedProducts(int buyerId)
    {
        var order = await _orderRepository.GetCurrentForBuyerWithOrderedProducts(buyerId);
        
        return ObjectMapper.Mapper.Map<OrderWithOrderedProductsAndBuyerAndSellerDTO>(order);
    }

    public async Task<OrderWithOrderedProductsAndBuyerAndSellerDTO> GetCurrentForSellerWithOrderedProducts(int sellerId)
    {
        var order = await _orderRepository.GetCurrentForSellerWithOrderedProducts(sellerId);
        
        return ObjectMapper.Mapper.Map<OrderWithOrderedProductsAndBuyerAndSellerDTO>(order);
    }

    public async Task<IEnumerable<OrderWithOrderedProductsAndBuyerAndSellerDTO?>> GetAllWithOrderedProducts()
    {
        var orders = await _orderRepository.GetAllWithOrderedProducts();
        
        return ObjectMapper.Mapper.Map<IEnumerable<OrderWithOrderedProductsAndBuyerAndSellerDTO>>(orders);
    }

    public async Task<IEnumerable<OrderWithOrderedProductsAndBuyerAndSellerDTO?>> GetAllPastForBuyer(int buyerId)
    {
        var orders = await _orderRepository.GetAllPastForBuyer(buyerId);

        return ObjectMapper.Mapper.Map<IEnumerable<OrderWithOrderedProductsAndBuyerAndSellerDTO>>(orders);
    }

    public async Task<IEnumerable<OrderWithOrderedProductsAndBuyerAndSellerDTO?>> GetAllPastForSeller(int sellerId)
    {
        var orders = await _orderRepository.GetAllPastForSeller(sellerId);

        return ObjectMapper.Mapper.Map<IEnumerable<OrderWithOrderedProductsAndBuyerAndSellerDTO>>(orders);
    }

    public async Task<IEnumerable<OrderWithOrderedProductsAndBuyerAndSellerDTO?>> GetAllPendingOrders()
    {
        var orders = await _orderRepository.GetAllPendingOrders();

        return ObjectMapper.Mapper.Map<IEnumerable<OrderWithOrderedProductsAndBuyerAndSellerDTO>>(orders);
    }

    public async Task<OrderWithOrderedProductsAndBuyerAndSellerDTO?> AcceptDeliveryOfOrder(int orderId, int sellerId)
    {
        var order = await GetById(orderId);

        if (order.DeliveryStatus != DeliveryStatus.Pending)
            throw new BusinessException("Delivery status must be Pending", 400);
        if(order.SellerId != null)
            throw new BusinessException("This order is accepted by different seller", 400);

        var sellerCurrentOrder = await GetCurrentForSellerWithOrderedProducts(sellerId);
        if(sellerCurrentOrder != null)
            throw new BusinessException("Seller already accepted different order ", 400);
        
        order.SellerId = sellerId;
        order.DeliveryStatus = DeliveryStatus.Accepted;
        order.DeliveryDateTime = DateTime.Now.AddSeconds(new Random().Next(10, 20));

        await Update(order);

        return ObjectMapper.Mapper.Map<OrderWithOrderedProductsAndBuyerAndSellerDTO>(order);
    }

    public async Task<OrderWithOrderedProductsAndBuyerAndSellerDTO?> FinishDeliveryOfOrder(int orderId)
    {
        var order = await GetById(orderId);

        if (order.DeliveryStatus != DeliveryStatus.Accepted)
            throw new BusinessException("Delivery status must be Accepted", 400);
        if(order.SellerId == null)
            throw new BusinessException("This order is not accepted by a seller", 400);

        order.DeliveryStatus = DeliveryStatus.Delivered;

        await Update(order);

        return ObjectMapper.Mapper.Map<OrderWithOrderedProductsAndBuyerAndSellerDTO>(order);
    }

    public async Task<IEnumerable<OrderDTO?>> GetAll()
    {
        var orders = await _orderRepository.GetAll();

        return ObjectMapper.Mapper.Map<IEnumerable<OrderDTO>>(orders);
    }

    public async Task<OrderWithOrderedProductsAndBuyerAndSellerDTO> CreateWithOrderedProducts(OrderCreateDTO order)
    {
        if (order.BuyerId <= 0) throw new BusinessException("Buyer ID is mandatory field", 400);
        if(order.OrderedProducts.Count() <= 0) 
            throw new BusinessException("Order must have at least one product", 400);
        if (order.OrderedProducts.Any(e => e.Amount <= 0))
            throw new BusinessException("All products must have amount of at least 1", 400);
        

        await _userService.GetById(order.BuyerId);
        
        var currentOrder = await GetCurrentForBuyerWithOrderedProducts(order.BuyerId);
        if(currentOrder != null)
            throw new BusinessException("Buyer cannot make new Order while they have a pending Order", 400);
        

        decimal fullPrice = await getFullPriceFromOrderedProducts(order.OrderedProducts);

        OrderDTO newOrder = await Create(new OrderDTO()
        {
            BuyerId = order.BuyerId,
            Comment = order.Comment,
            FullPrice = fullPrice + DELIVERY_CHARGE,
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
        
        return ObjectMapper.Mapper.Map<OrderWithOrderedProductsAndBuyerAndSellerDTO>(orderToReturn);
    }

    private async Task<decimal> getFullPriceFromOrderedProducts(IEnumerable<OrderedProductCreateDTO> orderedProducts)
    {
        decimal fullPrice = 0;
        
        foreach (var orderedProduct in orderedProducts)
        {
            var product = await _productService.GetById(orderedProduct.ProductId);

            fullPrice += product.Price * orderedProduct.Amount;
        }

        return fullPrice;
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
        orderFound.SellerId = order.SellerId;
        orderFound.DeliveryAddress = order.DeliveryAddress;
        orderFound.Comment = order.Comment;
        orderFound.FullPrice = order.FullPrice;
        orderFound.DeliveryStatus = order.DeliveryStatus;
        orderFound.DeliveryDateTime = order.DeliveryDateTime;
        
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