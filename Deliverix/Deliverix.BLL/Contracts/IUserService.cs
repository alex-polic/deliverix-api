using Deliverix.BLL.DTOs;

namespace Deliverix.BLL.Contracts;

public interface IUserService
{
    public Task<UserDTO> GetById(int id);
    public Task<IEnumerable<UserDTO>> GetAll();
    public Task<UserDTO> Create(UserDTO user);
    public Task<UserDTO> Update(UserDTO user);
    public Task<UserDTO> Delete(int id);
}