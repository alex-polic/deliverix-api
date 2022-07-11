using AutoMapper;
using Deliverix.BLL.DTOs;
using Deliverix.DAL.Models;

namespace Deliverix.BLL.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
    }
}