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
            CreateMap<Restaurant, RestaurantDtoResponse>()
                .ReverseMap();

            CreateMap<IPagedList<Restaurant>, IPagedList<RestaurantDtoResponse>>()
                    .ConvertUsing<PagedListConverter<Restaurant, RestaurantDtoResponse>>();
        }
    }
}