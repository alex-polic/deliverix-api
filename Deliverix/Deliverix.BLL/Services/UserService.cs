using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Deliverix.DAL;
using Deliverix.DAL.Contracts;
using Deliverix.DAL.Repositories;

namespace Deliverix.BLL.Services;

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private UnitOfWork _context;

    public UserService()
    {
        _userRepository = new UserRepository();
        _context = new UnitOfWork();
    }
    
    public async Task<UserDTO> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserDTO>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<UserDTO> Create(UserDTO user)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDTO> Update(UserDTO user)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDTO> Delete(int id)
    {
        throw new NotImplementedException();
    }
}