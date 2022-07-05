using Deliverix.BLL.DTOs.Internal;

namespace Deliverix.BLL.Contracts.Internal;

public interface IAuthService
{
    public Task<string> Login(string email, string password);
    public Task<string> Register(UserRegisterDTO userData);
}