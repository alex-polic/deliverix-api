using AutoMapper;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.DTOs.Internal;
using Deliverix.DAL.Models;

namespace Deliverix.BLL.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Order, OrderWithOrderedProductsAndBuyerAndCourierDTO>().ReverseMap();
        CreateMap<Order, OrderDTO>().ReverseMap();
        CreateMap<OrderDTO, OrderWithOrderedProductsAndBuyerAndCourierDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<OrderedProduct, OrderedProductDTO>().ReverseMap();
    }
}