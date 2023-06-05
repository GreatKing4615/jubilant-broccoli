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
            CreateMap<SignInRequest, User>().ForAllMembers(x => x.Ignore());

            CreateMap<LoginRequest, User>().ForAllMembers(x => x.Ignore());

            CreateMap<User, UserDto>()
                .ForMember(x => x.Id, x => x.MapFrom(opt => opt.Id))
                .ForMember(x => x.Email, x => x.MapFrom(opt => opt.Email))
                .ForMember(x => x.UserName, x => x.MapFrom(opt => opt.UserName))
                .ForMember(x => x.Address, x => x.MapFrom(opt => opt.Address));

            CreateMap<IPagedList<User>, IPagedList<UserDto>>()
                .ConvertUsing<PagedListConverter<User, UserDto>>();
        }
    }
}