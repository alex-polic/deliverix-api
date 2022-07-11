using Deliverix.BLL.Contracts;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.Mappers;
using Deliverix.Common.Exceptions;
using Deliverix.DAL;
using Deliverix.DAL.Contracts;
using Deliverix.DAL.Models;
using Deliverix.DAL.Repositories;

namespace Deliverix.BLL.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly UnitOfWork _context;
    public ProductService()
    {
        _context = new UnitOfWork();
        _productRepository = new ProductRepository(_context);
    }
    public async Task<ProductDTO> GetById(int id)
    {
        var product = await _productRepository.GetById(id);
        
        if (product == null)
            throw new BusinessException("Product with given ID not found", 400);
        
        return ObjectMapper.Mapper.Map<ProductDTO>(product);
    }

    public async Task<IEnumerable<ProductDTO?>> GetAll()
    {
        var products = await _productRepository.GetAll();

        return ObjectMapper.Mapper.Map<IEnumerable<ProductDTO>>(products);
    }

    public async Task<ProductDTO> Create(ProductDTO product)
    {
        Product model = ObjectMapper.Mapper.Map<Product>(product);
        
        Product result = await _productRepository.Create(model);

        await _context.SaveChangesAsync();
        
        return ObjectMapper.Mapper.Map<ProductDTO>(result);
    }

    public async Task<ProductDTO> Update(ProductDTO product)
    {
        ProductDTO productFound = await GetById(product.Id);
        
        if (productFound == null)
            throw new BusinessException("Product with given ID does not exist", 400);

        productFound.Name = product.Name;
        productFound.Price = product.Price;
        productFound.IngredientsDescription = product.IngredientsDescription;
        
        Product model = ObjectMapper.Mapper.Map<Product>(productFound);
        
        Product result = await _productRepository.Update(model);

        await _context.SaveChangesAsync();
        
        return ObjectMapper.Mapper.Map<ProductDTO>(result);
    }

    public async Task<ProductDTO> Delete(int id)
    {
        var product = await _productRepository.Delete(id);
        
        await _context.SaveChangesAsync();
        
        return ObjectMapper.Mapper.Map<ProductDTO>(product);
    }
}