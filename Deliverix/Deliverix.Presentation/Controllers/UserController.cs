using Deliverix.BLL.Contracts;
using Deliverix.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Deliverix.Presentation.Controllers;

[Route("[controller]/[action]")]
public class UserController : Controller
{
    private IUserService _service;
    
    public UserController()
    {
        _service = new UserService();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _service.GetById(id);

        return Json(user);
    }
}