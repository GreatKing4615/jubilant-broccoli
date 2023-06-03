using AutoMapper;
using JubilantBroccoli.Domain.Dtos.Item;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;

namespace JubilantBroccoli.MapperProfiles
{
    public class ItemOptionMapperConfiguration : Profile
    {
        public ItemOptionMapperConfiguration()
        {
            CreateMap<ItemOptionDto, ItemOption>()
                .ReverseMap();


            CreateMap<IPagedList<ItemOption>, IPagedList<ItemOptionDto>>()
                .ConvertUsing<PagedListConverter<ItemOption, ItemOptionDto>>();
        }
    }
}