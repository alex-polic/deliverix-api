using Deliverix.DAL.Models;

namespace Deliverix.DAL.Contracts;

public interface IUserRepository
{
    public Task<User?> GetByEmail(string email);
    public Task<User?> GetById(int id);
    public Task<IEnumerable<User?>> GetAll();
    public Task<User> Create(User user);
    public Task<User> Update(User user);
    public Task<User> Delete(int id);
}