using Deliverix.DAL.Contracts;
using Deliverix.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Deliverix.DAL.Repositories;

public class UserRepository : IUserRepository
{
    
    private DbContext _context;
    private DbSet<User> _collection;
    
    public UserRepository(UnitOfWork _unit = null)
    {
        if (_unit == null)
            _unit = new UnitOfWork();

        _context = _unit.Context;
        _collection = _context.Set<User>();
    }

    public async Task<User> GetById(int id)
    {
        return await _collection.AsNoTracking().FirstAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _collection.AsNoTracking().ToListAsync();
    }

    public async Task<User> Create(User user)
    {
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        await _collection.AddAsync(user);

        return user;
    }

    public async Task<User> Update(User user)
    {
        user.UpdatedAt = DateTime.UtcNow;
            
        _context.Entry(user).State = EntityState.Modified;

        return user;
    }

    public async Task<User> Delete(int id)
    {
        User found = await _collection.FirstAsync(e => e.Id == id);

        _collection.Remove(found);

        return found;
    }
}