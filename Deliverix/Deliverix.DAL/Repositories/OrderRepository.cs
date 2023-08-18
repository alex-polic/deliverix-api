using Deliverix.Common.Enums;
using Deliverix.DAL.Contracts;
using Deliverix.DAL.Models;
using Deliverix.DAL.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Deliverix.DAL.Repositories;

public class OrderRepository : IOrderRepository
{
    private DbContext _context;
    private DbSet<Order> _collection;
    private OrderValidator _validator;
    
    public OrderRepository(UnitOfWork _unit = null)
    {
        if (_unit == null)
            _unit = new UnitOfWork();

        _context = _unit.Context;
        _collection = _context.Set<Order>();
        _validator = new OrderValidator();
    }
    
    public async Task<Order?> GetById(int id)
    {
        return await _collection.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Order?> GetByIdWithOrderedProducts(int id)
    {
        return await _collection
            .Include(e => e.OrderedProducts)
            .Include(e => e.Buyer)
            .Include(e => e.Seller)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Order?> GetCurrentForBuyerWithOrderedProducts(int buyerId)
    {
        return await _collection.AsNoTracking()
            .FirstOrDefaultAsync(
                e => 
                    e.BuyerId == buyerId && e.DeliveryStatus != DeliveryStatus.Delivered
            );
    }

    public async Task<Order?> GetCurrentForSellerWithOrderedProducts(int sellerId)
    {
        return await _collection.AsNoTracking()
                .FirstOrDefaultAsync(
                    e => 
                        e.SellerId == sellerId && e.DeliveryStatus != DeliveryStatus.Delivered
                );
    }

    public async Task<IEnumerable<Order?>> GetAll()
    {
        return await _collection.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Order?>> GetAllWithOrderedProducts()
    {
        return await _collection
            .Include(e => e.OrderedProducts)
            .Include(e => e.Buyer)
            .Include(e => e.Seller)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Order?>> GetAllPastForBuyer(int buyerId)
    {
        return await _collection
            .Include(e => e.OrderedProducts)
            .Include(e => e.Buyer)
            .Include(e => e.Seller)
            .AsNoTracking()
            .Where(e => e.BuyerId == buyerId && e.DeliveryStatus == DeliveryStatus.Delivered)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order?>> GetAllPastForSeller(int sellerId)
    {
        return await _collection
            .Include(e => e.OrderedProducts)
            .Include(e => e.Buyer)
            .Include(e => e.Seller)
            .AsNoTracking()
            .Where(e => e.SellerId == sellerId && e.DeliveryStatus == DeliveryStatus.Delivered)
            .ToListAsync();    
    }
    
    public async Task<IEnumerable<Order?>> GetAllPendingOrders()
    {
        return await _collection
            .Include(e => e.OrderedProducts)
            .Include(e => e.Buyer)
            .Include(e => e.Seller)
            .AsNoTracking()
            .Where(e => e.SellerId == null && e.DeliveryStatus == DeliveryStatus.Pending)
            .ToListAsync();    
    }

    public async Task<Order> Create(Order order)
    {
        await _validator.ValidateAndThrowAsync(order);

        order.CreatedAt = DateTime.UtcNow;
        order.UpdatedAt = DateTime.UtcNow;
        
        await _collection.AddAsync(order);
        
        return order;
    }

    public async Task<Order> Update(Order order)
    {
        order.UpdatedAt = DateTime.UtcNow;
            
        _context.Entry(order).State = EntityState.Modified;

        return order;
    }

    public async Task<Order> Delete(int id)
    {
        var found = await _collection.FirstAsync(e => e.Id == id);

        _collection.Remove(found);

        return found;
    }
}