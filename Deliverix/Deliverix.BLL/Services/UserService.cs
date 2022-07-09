using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.Mappers;
using Deliverix.Common.Exceptions;
using Deliverix.DAL;
using Deliverix.DAL.Contracts;
using Deliverix.DAL.Models;
using Deliverix.DAL.Repositories;

namespace Deliverix.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private UnitOfWork _context;

    public UserService()
    {
        _context = new UnitOfWork();
        _userRepository = new UserRepository(_context);
    }

    public async Task<UserDTO> GetByEmail(string email)
    {
        var user = await _userRepository.GetByEmail(email);
        
        if (user == null)
            throw new BusinessException("User with given ID not found", 400);
        
        return ObjectMapper.Mapper.Map<UserDTO>(user);
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
        var users = await _userRepository.GetAll();

        return ObjectMapper.Mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public async Task<UserDTO> Create(UserDTO user)
    {
        User model = ObjectMapper.Mapper.Map<User>(user);
        
        User result = await _userRepository.Create(model);

        await _context.SaveChangesAsync();
        
        return ObjectMapper.Mapper.Map<UserDTO>(result);
    }

    public async Task<UserDTO> Update(UserDTO user)
    {
        UserDTO userFound = await GetById(user.Id);
        
        if (userFound == null)
            throw new BusinessException("User with given ID does not exist", 400);

        userFound.Username = user.Username;
        userFound.Email = user.Email;
        userFound.FullName = user.FullName;
        userFound.DateOfBirth = user.DateOfBirth;
        userFound.Address = user.Address;
        userFound.VerificationStatus = user.VerificationStatus != null ?
                user.VerificationStatus :
                userFound.VerificationStatus;
        
        User model = ObjectMapper.Mapper.Map<User>(userFound);
        
        User result = await _userRepository.Update(model);

        await _context.SaveChangesAsync();
        
        return ObjectMapper.Mapper.Map<UserDTO>(result);
    }

    public async Task<UserDTO> Delete(int id)
    {
        User? user = await _userRepository.Delete(id);

        return ObjectMapper.Mapper.Map<UserDTO>(user);
    }
}