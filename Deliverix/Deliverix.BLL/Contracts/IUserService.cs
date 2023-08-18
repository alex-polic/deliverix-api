using Deliverix.BLL.DTOs;

namespace Deliverix.BLL.Contracts;

public interface IUserService
{
    public Task<UserDTO> GetByEmail(string email);
    public Task<UserDTO> GetById(int id);
    public Task<IEnumerable<UserDTO>> GetAll();
    public Task<IEnumerable<UserDTO>> GetAllSellers();
    public Task<UserDTO> Create(UserDTO user);
    public Task<UserDTO> Update(UserDTO user);
    public Task<UserDTO> Delete(int id);
    public Task<UserDTO> ApproveVerification(int sellerId);
    public Task<UserDTO> RejectVerification(int sellerId);
}