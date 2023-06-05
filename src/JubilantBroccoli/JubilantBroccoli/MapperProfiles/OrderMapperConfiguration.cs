using AutoMapper;
using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Dtos.Item;
using JubilantBroccoli.Domain.Dtos.Order;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;

namespace JubilantBroccoli.MapperProfiles;

public class OrderMapperConfiguration : Profile
{
    public OrderMapperConfiguration()
    {
        CreateMap<OrderedItem, OrderItemDtoResponse>()
            .ForMember(x => x.Name, y => y.MapFrom(q => q.Item.Name))
            .ForMember(x => x.Price, y => y.MapFrom(q => q.Item.Price))
            .ForMember(x => x.Weight, y => y.MapFrom(q => q.Item.Weight))
            .ForMember(x => x.CookingTime, y => y.MapFrom(q => q.Item.CookingTime))
            .ForMember(x => x.Unit, y => y.MapFrom(q => q.Item.Unit))
            .ForMember(x => x.Type, y => y.MapFrom(q => q.Item.Type))
            .ForMember(x => x.ItemOptions, y => y.MapFrom(q => q.ItemOptions))
            .ForMember(x => x.Id, y => y.MapFrom(q => q.Id))
            .ReverseMap();

        CreateMap<Order, OrderDtoResponse>()
            .ForMember(x => x.Restaurant, y => y.MapFrom(q => q.RestaurantId))
            .ForMember(dest => dest.AverageTimeToReady, opt => opt.MapFrom(src => CalculateAverageCookingTime(src.OrderedItems)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(OrderStatus), src.Status)))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderedItems))
            .ForMember(dest => dest.DeliveryType,
                opt => opt.MapFrom(src => Enum.GetName(typeof(DeliveryType), src.DeliveryType)));

        CreateMap<IPagedList<Order>, IPagedList<OrderDtoResponse>>()
            .ConvertUsing<PagedListConverter<Order, OrderDtoResponse>>();

        CreateMap<IPagedList<OrderedItem>, IPagedList<OrderItemDtoResponse>>()
            .ConvertUsing<PagedListConverter<OrderedItem, OrderItemDtoResponse>>();
    }

    private TimeSpan CalculateAverageCookingTime(List<OrderedItem> items)
    {
        double totalCookingTimeTicks = 0;
        int itemCount = 0;

        foreach (var item in items)
        {
            totalCookingTimeTicks += item.Item.CookingTime.Ticks;
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