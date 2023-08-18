using System.Security.Claims;
using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.DTOs.Requests;
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
    public async Task<IActionResult> GetAllWithOrderedProducts()
    {
        var orders = await _service.GetAllWithOrderedProducts();

        return Json(orders);
    }
    
    [HttpGet]
    [Authorize(Policy = "Buyer")]
    public async Task<IActionResult> GetCurrentForBuyerWithOrderedProducts()
    {
        int buyerId = int.Parse(HttpContext.User.Claims.First(e => e.Type == ClaimTypes.Actor).Value);
        
        var orders = await _service.GetCurrentForBuyerWithOrderedProducts(buyerId);

        return Json(orders);
    }
    
    [HttpGet]
    [Authorize(Policy = "Seller")]
    public async Task<IActionResult> GetCurrentForSellerWithOrderedProducts()
    {
        int sellerId = int.Parse(HttpContext.User.Claims.First(e => e.Type == ClaimTypes.Actor).Value);
        
        var orders = await _service.GetCurrentForSellerWithOrderedProducts(sellerId);

        return Json(orders);
    }
    
    [HttpGet]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetAll();

        return Json(orders);
    }
    
    [HttpGet]
    [Authorize(Policy = "Buyer")]
    public async Task<IActionResult> GetAllPastForBuyer(int buyerId)
    {
        var orders = await _service.GetAllPastForBuyer(buyerId);

        return Json(orders);
    }
    
    [HttpGet]
    [Authorize(Policy = "Seller")]
    public async Task<IActionResult> GetAllPastForSeller(int sellerId)
    {
        var orders = await _service.GetAllPastForSeller(sellerId);

        return Json(orders);
    }
    
    [HttpGet]
    [Authorize(Policy = "Seller")]
    public async Task<IActionResult> GetAllPendingOrders()
    {
        var orders = await _service.GetAllPendingOrders();

        return Json(orders);
    }
    
    [HttpPost]
    [Authorize(Policy = "Seller")]
    public async Task<IActionResult> AcceptDeliveryOfOrder(int orderId)
    {
        int sellerId = int.Parse(HttpContext.User.Claims.First(e => e.Type == ClaimTypes.Actor).Value);

        var orders = await _service.AcceptDeliveryOfOrder(orderId, sellerId);

        return Json(orders);
    }
    
    [HttpPost]
    [Authorize(Policy = "Any")]
    public async Task<IActionResult> FinishDeliveryOfOrder(int orderId)
    {
        var orders = await _service.FinishDeliveryOfOrder(orderId);

        return Json(orders);
    }
    
    [HttpPost]
    [Authorize(Policy = "Buyer")]
    public async Task<IActionResult> Create([FromBody] OrderDTO order)
    {
        var orderCreated = await _service.Create(order);

        return Json(orderCreated);
    }
    
    [HttpPost]
    [Authorize(Policy = "Buyer")]
    public async Task<IActionResult> CreateWithOrderedProducts([FromBody] OrderCreateDTO order)
    {
        var orderCreated = await _service.CreateWithOrderedProducts(order);

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