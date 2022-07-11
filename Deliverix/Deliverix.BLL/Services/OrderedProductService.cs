using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.Mappers;
using Deliverix.Common.Exceptions;
using Deliverix.DAL;
using Deliverix.DAL.Contracts;
using Deliverix.DAL.Models;
using Deliverix.DAL.Repositories;

namespace Deliverix.BLL.Services;

public class OrderedProductService : IOrderedProductService
{
    private readonly IOrderedProductRepository _orderedProductRepository;
    private readonly UnitOfWork _context;
    public OrderedProductService()
    {
        _context = new UnitOfWork();
        _orderedProductRepository = new OrderedProductRepository(_context);
    }
    public async Task<OrderedProductDTO> GetById(int id)
    {
        var orderedProduct = await _orderedProductRepository.GetById(id);
        
        if (orderedProduct == null)
            throw new BusinessException("OrderedProduct with given ID not found", 400);
        
        return ObjectMapper.Mapper.Map<OrderedProductDTO>(orderedProduct);
    }

    public async Task<IEnumerable<OrderedProductDTO?>> GetAll()
    {
        var orderedProducts = await _orderedProductRepository.GetAll();

        return ObjectMapper.Mapper.Map<IEnumerable<OrderedProductDTO>>(orderedProducts);
    }

    public async Task<OrderedProductDTO> Create(OrderedProductDTO orderedProduct)
    {
        OrderedProduct model = ObjectMapper.Mapper.Map<OrderedProduct>(orderedProduct);
        
        OrderedProduct result = await _orderedProductRepository.Create(model);

        await _context.SaveChangesAsync();
        
        return ObjectMapper.Mapper.Map<OrderedProductDTO>(result);
    }

    public async Task<OrderedProductDTO> Update(OrderedProductDTO orderedProduct)
    {
        OrderedProductDTO orderedProductFound = await GetById(orderedProduct.Id);
        
        if (orderedProductFound == null)
            throw new BusinessException("OrderedProduct with given ID does not exist", 400);

        orderedProductFound.OrderId = orderedProduct.OrderId;
        orderedProductFound.ProductId = orderedProduct.ProductId;
        orderedProductFound.Amount = orderedProduct.Amount;

        OrderedProduct model = ObjectMapper.Mapper.Map<OrderedProduct>(orderedProductFound);
        
        OrderedProduct result = await _orderedProductRepository.Update(model);

        await _context.SaveChangesAsync();
        
        return ObjectMapper.Mapper.Map<OrderedProductDTO>(result);
    }

    public async Task<OrderedProductDTO> Delete(int id)
    {
        var orderedProduct = await _orderedProductRepository.Delete(id);
        
        await _context.SaveChangesAsync();
        
        return ObjectMapper.Mapper.Map<OrderedProductDTO>(orderedProduct);
    }
}