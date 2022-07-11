using AutoMapper;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.DTOs.Internal;
using Deliverix.DAL.Models;

namespace Deliverix.BLL.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Order, OrderDTO>().ReverseMap();
        CreateMap<Order, OrderWithOrderedProductsDTO>().ReverseMap();
        CreateMap<OrderDTO, OrderWithOrderedProductsDTO>().ReverseMap();
        CreateMap<OrderedProduct, OrderedProductDTO>().ReverseMap();
    }
}