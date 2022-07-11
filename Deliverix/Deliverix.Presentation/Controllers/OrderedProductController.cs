using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliverix.Presentation.Controllers;

[Route("[controller]/[action]")]
public class OrderedProductController : Controller
{
    private readonly IOrderedProductService _service;
    
    public OrderedProductController(IOrderedProductService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> GetById(int id)
    {
        var orderedProduct = await _service.GetById(id);

        return Json(orderedProduct);
    }
    
    [HttpGet]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> GetAll()
    {
        var orderedProducts = await _service.GetAll();

        return Json(orderedProducts);
    }
    
    [HttpPost]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> Create([FromBody] OrderedProductDTO orderedProduct)
    {
        var orderedProductCreated = await _service.Create(orderedProduct);

        return Json(orderedProductCreated);
    }
    
    [HttpPatch]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> Update([FromBody] OrderedProductDTO orderedProduct)
    {
        var orderedProductUpdated = await _service.Update(orderedProduct);

        return Json(orderedProductUpdated);
    }
    
    [HttpDelete]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> Delete(int id)
    {
        var orderedProduct = await _service.Delete(id);

        return Json(orderedProduct);
    }
}