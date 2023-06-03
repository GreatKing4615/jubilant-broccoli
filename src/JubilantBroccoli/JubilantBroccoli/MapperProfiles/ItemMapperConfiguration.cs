using AutoMapper;
using JubilantBroccoli.Domain.Dtos.Item;
using JubilantBroccoli.Domain.Models;

namespace JubilantBroccoli.MapperProfiles;

public class ItemMapperConfiguration : Profile
{
    public ItemMapperConfiguration()
    {
        CreateMap<Item, ItemDto>().ReverseMap();
    }
}