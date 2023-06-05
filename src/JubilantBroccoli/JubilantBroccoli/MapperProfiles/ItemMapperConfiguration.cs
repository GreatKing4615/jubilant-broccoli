using AutoMapper;
using JubilantBroccoli.Domain.Dtos.Item;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;

namespace JubilantBroccoli.MapperProfiles;

public class ItemMapperConfiguration : Profile
{
    public ItemMapperConfiguration()
    {
        CreateMap<Item, ItemDtoResponse>();

        CreateMap<IPagedList<Item>, IPagedList<ItemDtoResponse>>()
            .ConvertUsing<PagedListConverter<Item, ItemDtoResponse>>();
    }
}