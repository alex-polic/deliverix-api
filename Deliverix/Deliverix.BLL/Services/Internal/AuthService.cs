using Deliverix.BLL.Contracts;
using Deliverix.BLL.Contracts.Internal;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.DTOs.Internal;
using Deliverix.Common.Exceptions;
using Deliverix.Common.Helpers;

namespace Deliverix.BLL.Services.Internal;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    public AuthService()
    {
        _userService = new UserService();
    }
    
    public async Task Login(string email, string password)
    {
        throw new NotImplementedException();
    }

    public async Task Register(UserRegisterDTO userData)
    {
        if (userData.Password != userData.PasswordConfirmation)
            throw new BusinessException("Passwords do not match", 400);

        UserDTO newUser = new UserDTO()
        {
            Username = userData.Username,
            Email = userData.Email,
            Password = HashHelper.Hash(userData.Password),
            FullName = userData.FullName,
            DateOfBirth = userData.DateOfBirth,
            Address = userData.Address,
            UserType = userData.UserType,
            ProfilePictureUrl = userData.ProfilePictureUrl
        };

        await _userService.Create(newUser);
    }
}