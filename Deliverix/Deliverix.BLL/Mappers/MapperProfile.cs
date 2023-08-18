using AutoMapper;
using Deliverix.BLL.DTOs;
using Deliverix.BLL.DTOs.Internal;
using Deliverix.DAL.Models;

namespace Deliverix.BLL.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Order, OrderWithOrderedProductsAndBuyerAndSellerDTO>().ReverseMap();
        CreateMap<Order, OrderDTO>().ReverseMap();
        CreateMap<OrderDTO, OrderWithOrderedProductsAndBuyerAndSellerDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<OrderedProduct, OrderedProductDTO>().ReverseMap();
    }
}