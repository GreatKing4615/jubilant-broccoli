using JubilantBroccoli.Domain.Dtos.User;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.Core.Interfaces;
using JubilantBroccoli.Infrastructure.Mappers.Base;

namespace JubilantBroccoli.Infrastructure.Mappers
{
    public class UserMapperConfiguration: MapperConfigurationBase
    {
        public UserMapperConfiguration()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<IPagedList<User>, IPagedList<UserDto>>()
                .ConvertUsing<PagedListConverter<User, UserDto>>();
        }
    }
}