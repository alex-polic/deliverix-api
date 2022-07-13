using System.Security.Claims;
using Deliverix.BLL.Contracts.Internal;
using Deliverix.BLL.DTOs.Internal;
using Deliverix.BLL.DTOs.Requests;
using Deliverix.BLL.Services.Internal;
using Deliverix.Common.Enums;
using Deliverix.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliverix.Presentation.Controllers;

[Route("[controller]/[action]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController()
    {
        _authService = new AuthService();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDTO request)
    {
        var token = await _authService.Login(request.Email, request.Password);
        return Json(token);
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(
        string username,
        string email,
        string password,
        string passwordConfirmation,
        string fullName,
        DateTime dateOfBirth,
        string address,
        int userType,
        IFormFile profilePicture
    )
    {
        string profilePictureUrl = UploadHelper.UploadImage(profilePicture);
        
        string token = await _authService.Register(new UserRegisterDTO()
        {
            Username = username,
            Email = email,
            Password = password,
            PasswordConfirmation = passwordConfirmation,
            FullName = fullName,
            DateOfBirth = dateOfBirth,
            Address = address,
            UserType = (UserType) userType,
            ProfilePictureUrl = profilePictureUrl
        });
        
        return Json(new { Message = "Success", Token = token });
    }

    [HttpGet]
    [Authorize(Policy = "Any")]
    public IActionResult GetUserData()
    {
        var id = HttpContext.User.Claims.First(e => e.Type == ClaimTypes.Actor).Value;
        var email = HttpContext.User.Claims.First(e => e.Type == ClaimTypes.Email).Value;
        var role = HttpContext.User.Claims.First(e => e.Type == ClaimTypes.Role).Value;
        var verificationStatus = HttpContext.User.Claims.First(e => e.Type == "verificationStatus").Value;

        return Json(new { id, email, role, verificationStatus });
    }
}