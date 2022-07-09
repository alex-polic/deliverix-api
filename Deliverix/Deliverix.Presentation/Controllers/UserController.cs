using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _service.GetById(id);

        return Json(user);
    }
    
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var user = await _service.GetAll();

        return Json(user);
    }
    
    [HttpPatch]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> Update([FromBody] UserDTO user)
    {
        var userUpdated = await _service.Update(user);

        return Json(userUpdated);
    }
}