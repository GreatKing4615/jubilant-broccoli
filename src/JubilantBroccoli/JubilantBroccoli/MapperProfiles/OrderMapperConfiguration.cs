using AutoMapper;
using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Dtos.Item;
using JubilantBroccoli.Domain.Dtos.Order;
using JubilantBroccoli.Domain.Dtos.Restaurant;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;

namespace JubilantBroccoli.MapperProfiles;

public class OrderMapperConfiguration : Profile
{
    public OrderMapperConfiguration()
    {
        CreateMap<OrderItemDto, OrderedItem>()
            .ReverseMap();
        CreateMap<RestaurantDto, Order>()
            .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();
        CreateMap<RestaurantDto, Restaurant>()
            .ReverseMap();
        CreateMap<OrderDto, Order>()
            .IncludeMembers(src => src.Restaurant)
            .ForMember(dest => dest.OrderedItems, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(OrderStatus), src.Status)))
            .ForMember(dest => dest.DeliveryType, opt => opt.MapFrom(src => Enum.GetName(typeof(DeliveryType), src.DeliveryType)))

            .ForMember(dest => dest.DeliveryTime, opt => opt.MapFrom(src => CalculateAverageCookingTime(src.Items)))
            .ReverseMap()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderedItems));

        CreateMap<IPagedList<Order>, IPagedList<OrderDto>>()
            .ConvertUsing<PagedListConverter<Order, OrderDto>>();
    }

    private TimeSpan CalculateAverageCookingTime(List<OrderItemDto> items)
    {
        double totalCookingTimeTicks = 0;
        int itemCount = 0;

        foreach (var item in items)
        {
            totalCookingTimeTicks += item.CookingTime.Ticks;
            itemCount++;
        }

        if (itemCount > 0)
        {
            var averageCookingTimeTicks = totalCookingTimeTicks / itemCount;
            return TimeSpan.FromTicks((long)averageCookingTimeTicks);
        }

        return TimeSpan.Zero;
    }

}