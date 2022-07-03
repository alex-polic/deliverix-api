using Deliverix.BLL.DTOs.Internal;

namespace Deliverix.BLL.Contracts.Internal;

public interface IAuthService
{
    public Task Login(string email, string password);
    public Task Register(UserRegisterDTO userData);
}