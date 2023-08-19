using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deliverix.Presentation.Controllers;

[Route("[controller]/[action]")]
public class ProductController : Controller
{
    private readonly IProductService _service;
    
    public ProductController(IProductService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _service.GetById(id);

        return Json(product);
    }
    
    [HttpGet]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAll();

        return Json(products);
    }
    
    [HttpPost]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> Create([FromBody] ProductDTO product)
    {
        var productCreated = await _service.Create(product);

        return Json(productCreated);
    }
    
    [HttpPatch]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> Update([FromBody] ProductDTO product)
    {
        var productUpdated = await _service.Update(product);

        return Json(productUpdated);
    }
    
    [HttpDelete]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _service.Delete(id);

        return Json(product);
    }
}