using Deliverix.DAL.Contracts;
using Deliverix.DAL.Models;
using Deliverix.DAL.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Deliverix.DAL.Repositories;

public class ProductRepository : IProductRepository
{
    private DbContext _context;
    private DbSet<Product> _collection;
    private ProductValidator _validator;
    
    public ProductRepository(UnitOfWork _unit = null)
    {
        if (_unit == null)
            _unit = new UnitOfWork();

        _context = _unit.Context;
        _collection = _context.Set<Product>();
        _validator = new ProductValidator();
    }
    
    public async Task<Product?> GetById(int id)
    {
        return await _collection.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Product?>> GetAll()
    {
        return await _collection.AsNoTracking().ToListAsync();
    }

    public async Task<Product> Create(Product product)
    {
        await _validator.ValidateAndThrowAsync(product);

        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;
        
        await _collection.AddAsync(product);
        
        return product;
    }

    public async Task<Product> Update(Product product)
    {
        product.UpdatedAt = DateTime.UtcNow;
            
        _context.Entry(product).State = EntityState.Modified;

        return product;
    }

    public async Task<Product> Delete(int id)
    {
        var found = await _collection.FirstAsync(e => e.Id == id);

        _collection.Remove(found);

        return found;
    }
}