using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.Mappers;
using Deliverix.Common.Exceptions;
using Deliverix.DAL;
using Deliverix.DAL.Contracts;
using Deliverix.DAL.Repositories;

namespace Deliverix.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private UnitOfWork _context;

    public UserService()
    {
        _userRepository = new UserRepository();
        _context = new UnitOfWork();
    }
    
    public async Task<UserDTO> GetById(int id)
    {
        var user = await _userRepository.GetById(id);
        
        if (user == null)
            throw new BusinessException("User with given ID not found", 400);
        
        return ObjectMapper.Mapper.Map<UserDTO>(user);
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