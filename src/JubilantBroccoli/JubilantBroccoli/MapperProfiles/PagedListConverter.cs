using AutoMapper;
using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using JubilantBroccoli.Infrastructure.UnitOfWork.Implements;

namespace JubilantBroccoli.MapperProfiles;

public class PagedListConverter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>>
{
    public IPagedList<TDestination> Convert(
        IPagedList<TSource> source,
        IPagedList<TDestination> destination,
        ResolutionContext context)
    {
        return source == null
            ? PagedList.Empty<TDestination>()
            : PagedList.From(source, x => context.Mapper.Map<IEnumerable<TDestination>>(x));
    }
}