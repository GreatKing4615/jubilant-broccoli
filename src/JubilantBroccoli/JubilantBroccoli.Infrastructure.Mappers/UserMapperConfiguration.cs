using JubilantBroccoli.Domain.Dtos.Customer;
using JubilantBroccoli.Domain.Models;
using JubilantBroccoli.Infrastructure.Mappers.Base;
using JubilantBroccoli.Infrastructure.UnitOfWork.Interfaces;

namespace JubilantBroccoli.Infrastructure.Mappers
{
    public class UserMapperConfiguration: MapperConfigurationBase
    {
        public UserMapperConfiguration()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            //
            // CreateMap<FactUpdateViewModel, Fact>()
            //     .ForMember(x => x.Id, opt => opt.Ignore())
            //     .ForMember(x => x.CreatedAt, opt => opt.Ignore())
            //     .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //     .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
            //     .ForMember(x => x.UpdatedBy, opt => opt.Ignore())
            //     .ForMember(x => x.Tags, opt => opt.Ignore())
            //     .ReverseMap();

            CreateMap<IPagedList<Customer>, IPagedList<CustomerDto>>()
                .ConvertUsing<PagedListConverter<Customer, CustomerDto>>();
        }
    }
}