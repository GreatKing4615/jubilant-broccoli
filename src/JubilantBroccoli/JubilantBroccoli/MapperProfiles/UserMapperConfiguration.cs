using AutoMapper;
using JubilantBroccoli.Domain.Dtos.User;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;

namespace JubilantBroccoli.MapperProfiles
{
    public class UserMapperConfiguration : Profile
    {
        public UserMapperConfiguration()
        {
            CreateMap<UserDto, AuthenticationRequest>()
                .ForMember(x => x.Password, x => x.Ignore())
                .ReverseMap();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Id, x => x.MapFrom(opt => opt.Id))
                .ForMember(x => x.Email, x => x.MapFrom(opt => opt.Email))
                .ForMember(x => x.UserName, x => x.MapFrom(opt => opt.UserName))
                .ForAllMembers(x => x.Ignore());

            CreateMap<IPagedList<User>, IPagedList<UserDto>>()
                .ConvertUsing<PagedListConverter<User, UserDto>>();
        }
    }
}