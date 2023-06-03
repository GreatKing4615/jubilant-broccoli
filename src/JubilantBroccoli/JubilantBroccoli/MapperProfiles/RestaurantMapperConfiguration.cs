using AutoMapper;
using JubilantBroccoli.Domain.Dtos.Restaurant;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;

namespace JubilantBroccoli.MapperProfiles
{
    public class RestaurantMapperConfiguration : Profile
    {
        public RestaurantMapperConfiguration()
        {
            CreateMap<RestaurantDto, Restaurant>()
                .ForMember(x => x.Orders, x => x.Ignore())
                .ReverseMap();


            CreateMap<IPagedList<Restaurant>, IPagedList<RestaurantDto>>()
                .ConvertUsing<PagedListConverter<Restaurant, RestaurantDto>>();
        }
    }
}