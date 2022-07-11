using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliverix.Presentation.Controllers;

[Route("[controller]/[action]")]
public class OrderController : Controller
{
    private readonly IOrderService _service;
    
    public OrderController(IOrderService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _service.GetById(id);

        return Json(order);
    }
    
    [HttpGet]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetAll();

        return Json(orders);
    }
    
    [HttpPost]
    [Authorize(Policy = "Buyer")]
    public async Task<IActionResult> Create([FromBody] OrderDTO order)
    {
        var orderCreated = await _service.Create(order);

        return Json(orderCreated);
    }
    
    [HttpPatch]
    [Authorize(Policy = "Administrator")]
    public async Task<IActionResult> Update([FromBody] OrderDTO order)
    {
        var orderUpdated = await _service.Update(order);

        return Json(orderUpdated);
    }
    
    [HttpDelete]
    [Authorize(Policy = "Administrator")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _service.Delete(id);

        return Json(order);
    }
}