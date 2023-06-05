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
            CreateMap<ItemOption, ItemOptionDtoResponse>();
            CreateMap<ItemOption, OrderedItem>()
                .ForMember(x => x.Order, y => y.Ignore())
                .ForMember(x => x.Item, y => y.Ignore())
                .ForMember(x => x.Status, y => y.Ignore())
                .ForMember(x => x.Count, y => y.Ignore())
                .ForMember(x => x.UpdatedAt, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore())
                .ForMember(x => x.ItemOptions, y => y.Ignore())
                .ForMember(x => x.Id, y => y.MapFrom(q => q.Items.FirstOrDefault(x => x.Id == q.Id)))
                .ForMember(x => x.ItemId, y => y.MapFrom(q => q.Id))
                .ReverseMap();
            CreateMap<IPagedList<ItemOption>, IPagedList<ItemOptionDtoResponse>>()
                .ConvertUsing<PagedListConverter<ItemOption, ItemOptionDtoResponse>>();
        }
    }
}