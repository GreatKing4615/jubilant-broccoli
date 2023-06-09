﻿using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using JubilantBroccoli.Infrastructure.UnitOfWork.Implements;

namespace JubilantBroccoli.Infrastructure.UnitOfWork.Extensions;

public static class EnumerablePagedListExtensions
{
    public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize,
        int indexFrom = 0) => new PagedList<T>(source, pageIndex, pageSize, indexFrom);

    public static IPagedList<TResult> ToPagedList<TSource, TResult>(this IEnumerable<TSource> source,
        Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int pageIndex, int pageSize,
        int indexFrom = 0) => new PagedList<TSource, TResult>(source, converter, pageIndex, pageSize, indexFrom);
}