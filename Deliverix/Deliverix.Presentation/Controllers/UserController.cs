using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliverix.Presentation.Controllers;

[Route("[controller]/[action]")]
public class UserController : Controller
{
    private IUserService _service;
    
    public UserController(IUserService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _service.GetById(id);

        return Json(user);
    }
    
    [HttpGet]
    [Authorize(Policy = "Administrator")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _service.GetAll();

        return Json(users);
    }
    
    [HttpGet]
    [Authorize(Policy = "Administrator")]
    public async Task<IActionResult> GetAllSellers()
    {
        var users = await _service.GetAllSellers();

        return Json(users);
    }
    
    [HttpPatch]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> Update([FromBody] UserDTO user)
    {
        var userUpdated = await _service.Update(user);

        return Json(userUpdated);
    }
    
    [HttpPost]
    [Authorize(Policy = "Administrator")]
    public async Task<IActionResult> ApproveVerification([FromBody] VerificationDTO request)
    {
        var userUpdated = await _service.ApproveVerification(request.SellerId);

        return Json(userUpdated);
    }
    
    [HttpPost]
    [Authorize(Policy = "Administrator")]
    public async Task<IActionResult> RejectVerification([FromBody] VerificationDTO request)
    {
        var userUpdated = await _service.RejectVerification(request.SellerId);

        return Json(userUpdated);
    }
}