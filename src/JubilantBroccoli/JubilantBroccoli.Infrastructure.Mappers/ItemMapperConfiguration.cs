using JubilantBroccoli.Domain.Dtos.Item;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.Mappers.Base;

namespace JubilantBroccoli.Infrastructure.Mappers;

public class ItemMapperConfiguration: MapperConfigurationBase
{
    public ItemMapperConfiguration()
    {
        CreateMap<Item, ItemDto>().ReverseMap();
    }
}