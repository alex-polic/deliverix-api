using Deliverix.DAL.Contracts;
using Deliverix.DAL.Models;
using Deliverix.DAL.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Deliverix.DAL.Repositories;

public class OrderedProductRepository : IOrderedProductRepository
{
    private DbContext _context;
    private DbSet<OrderedProduct> _collection;
    private OrderedProductValidator _validator;
    
    public OrderedProductRepository(UnitOfWork _unit = null)
    {
        if (_unit == null)
            _unit = new UnitOfWork();

        _context = _unit.Context;
        _collection = _context.Set<OrderedProduct>();
        _validator = new OrderedProductValidator();
    }
    
    public async Task<OrderedProduct?> GetById(int id)
    {
        return await _collection.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<OrderedProduct?>> GetAll()
    {
        return await _collection.AsNoTracking().ToListAsync();
    }

    public async Task<OrderedProduct> Create(OrderedProduct orderedProduct)
    {
        await _validator.ValidateAndThrowAsync(orderedProduct);

        orderedProduct.CreatedAt = DateTime.UtcNow;
        orderedProduct.UpdatedAt = DateTime.UtcNow;
        
        await _collection.AddAsync(orderedProduct);
        
        return orderedProduct;
    }

    public async Task<OrderedProduct> Update(OrderedProduct orderedProduct)
    {
        orderedProduct.UpdatedAt = DateTime.UtcNow;
            
        _context.Entry(orderedProduct).State = EntityState.Modified;

        return orderedProduct;
    }

    public async Task<OrderedProduct> Delete(int id)
    {
        var found = await _collection.FirstAsync(e => e.Id == id);

        _collection.Remove(found);

        return found;
    }
}