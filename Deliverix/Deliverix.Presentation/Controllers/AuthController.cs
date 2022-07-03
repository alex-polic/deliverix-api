using Deliverix.BLL.DTOs.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Deliverix.Presentation.Controllers;

[Route("[controller]/[action]")]
public class AuthController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Login()
    {
        return Json(new { Message = "Success" });
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
    {
        return Json(new { Message = "Success" });
    }
}