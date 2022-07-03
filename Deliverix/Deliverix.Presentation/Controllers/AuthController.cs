using Microsoft.AspNetCore.Mvc;

namespace Deliverix.Presentation.Controllers;

[Route("[controller]/[action]")]
public class AuthController : Controller
{
    public async Task<IActionResult> Login()
    {
        return Json(new { Message = "Success" });
    }
    
    public async Task<IActionResult> Register()
    {
        return Json(new { Message = "Success" });
    }
}