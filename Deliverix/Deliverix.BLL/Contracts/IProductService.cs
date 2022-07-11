using Deliverix.BLL.DTOs;

namespace Deliverix.BLL.Contracts;

public interface IProductService
{
    public Task<ProductDTO> GetById(int id);
    public Task<IEnumerable<ProductDTO?>> GetAll();
    public Task<ProductDTO> Create(ProductDTO product);
    public Task<ProductDTO> Update(ProductDTO product);
    public Task<ProductDTO> Delete(int id);
}