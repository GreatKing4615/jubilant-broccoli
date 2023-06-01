using AutoMapper;
using JubilantBroccoli.Infrastructure.Core;
using JubilantBroccoli.Infrastructure.Core.Interfaces;

namespace JubilantBroccoli.Infrastructure.Mappers.Base;

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