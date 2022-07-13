using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Deliverix.BLL.Contracts;
using Deliverix.BLL.Contracts.Internal;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.DTOs.Internal;
using Deliverix.Common.Configurations;
using Deliverix.Common.Enums;
using Deliverix.Common.Exceptions;
using Deliverix.Common.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace Deliverix.BLL.Services.Internal;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    private JwtHeader _definedHeader;
    private JwtSecurityTokenHandler _jwtHandler;

    private string _serverPrivateKey;

    public AuthService()
    {
        _userService = new UserService();
        
        _serverPrivateKey = AppConfiguration.GetConfiguration("ServerSecret");

        _definedHeader = new JwtHeader(new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_serverPrivateKey)),
            SecurityAlgorithms.HmacSha256
        ));

        _jwtHandler = new JwtSecurityTokenHandler();
    }
    
    public async Task<string> Login(string email, string password)
    {
        return await GenerateTokenForUser(email, password);
    }

    public async Task<string> Register(UserRegisterDTO userData)
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
            VerificationStatus = userData.UserType == UserType.Buyer ? 
                VerificationStatus.Approved : 
                VerificationStatus.Pending,
            ProfilePictureUrl = userData.ProfilePictureUrl
        };

        await _userService.Create(newUser);

        return await GenerateTokenForUser(userData.Email, userData.Password);
    }
    
    private async Task<string> GenerateTokenForUser(string email, string password)
    {
        UserDTO found = await _userService.GetByEmail(email);

        if (found == null) throw new BusinessException("Credentials incorrect.", 400);
        if (found.Password != HashHelper.Hash(password)) throw new BusinessException("Credentials incorrect.", 400);

        JwtSecurityToken secToken = new JwtSecurityToken(
            signingCredentials: _definedHeader.SigningCredentials,
            claims: new List<Claim>(){
                new (ClaimTypes.Actor, found.Id.ToString()),
                new (ClaimTypes.Email, found.Email),
                new (ClaimTypes.Role, found.UserType.ToString()),
                new ("verificationStatus", found.VerificationStatus.ToString()),
                new ("issued at", DateTime.UtcNow.ToString())
            }
        );

        return _jwtHandler.WriteToken(secToken);
    }
}