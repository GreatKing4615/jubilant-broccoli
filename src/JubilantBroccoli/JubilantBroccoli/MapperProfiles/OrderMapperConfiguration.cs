using AutoMapper;
using JubilantBroccoli.Domain.Dtos.Order;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;

namespace JubilantBroccoli.MapperProfiles;

public class OrderMapperConfiguration : Profile
{
    public OrderMapperConfiguration()
    {
        CreateMap<OrderDto, Order>()
            .ReverseMap();

        CreateMap<IPagedList<Order>, IPagedList<OrderDto>>()
            .ConvertUsing<PagedListConverter<Order, OrderDto>>();
    }
}