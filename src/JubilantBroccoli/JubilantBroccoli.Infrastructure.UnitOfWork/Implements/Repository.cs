using JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;
using JubilantBroccoli.Infrastructure.UnitOfWork.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace JubilantBroccoli.Infrastructure.UnitOfWork.Implements;

public sealed class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = _dbContext.Set<TEntity>();
    }

    public Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int pageIndex = 0,
        int pageSize = 20,
        bool disableTracking = true,
        CancellationToken cancellationToken = default,
        bool ignoreQueryFilters = false,
        bool ignoreAutoIncludes = false)
        where TResult : class
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (ignoreAutoIncludes)
        {
            query = query.IgnoreAutoIncludes();
        }

        return orderBy != null
            ? orderBy(query).Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken)
            : query.Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
    }

    public async Task<TResult?> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true,
        CancellationToken cancellationToken = default,
        bool ignoreQueryFilters = false,
        bool ignoreAutoIncludes = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (ignoreAutoIncludes)
        {
            query = query.IgnoreAutoIncludes();
        }

        return orderBy is not null
            ? await orderBy(query).Select(selector).FirstOrDefaultAsync(cancellationToken)
            : await query.Select(selector).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TResult?> SingleOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true,
        CancellationToken token = default,
        bool ignoreQueryFilters = false,
        bool ignoreAutoIncludes = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (ignoreAutoIncludes)
        {
            query = query.IgnoreAutoIncludes();
        }

        return orderBy is not null
            ? await orderBy(query).Select(selector).FirstOrDefaultAsync(token)
            : await query.Select(selector).FirstOrDefaultAsync(token);
    }

    public ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken) => _dbSet.FindAsync(keyValues, cancellationToken);

    public IQueryable<TEntity> GetAll(bool disableTracking = true) =>
        disableTracking
            ? _dbSet.AsNoTracking()
            : _dbSet;

    public IQueryable<TResult?> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false,
        bool ignoreAutoIncludes = false) where TResult : class
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (ignoreAutoIncludes)
        {
            query = query.IgnoreAutoIncludes();
        }

        return orderBy is not null
            ? orderBy(query).Select(selector)
            : query.Select(selector);
    }

    public async Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default) =>
        predicate is null
            ? await _dbSet.CountAsync(cancellationToken)
            : await _dbSet.CountAsync(predicate, cancellationToken);

    public async Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>>? selector = null,
        CancellationToken cancellationToken = default) =>
        selector is null
            ? await _dbSet.AnyAsync(cancellationToken)
            : await _dbSet.AnyAsync(selector, cancellationToken);

    public Task<decimal> AverageAsync(
        Expression<Func<TEntity, decimal>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default) =>
        predicate is null
            ? _dbSet.AverageAsync(selector, cancellationToken)
            : _dbSet.Where(predicate).AverageAsync(selector, cancellationToken);

    public Task<decimal> SumAsync(
        Expression<Func<TEntity, decimal>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default) =>
        predicate is null
            ? _dbSet.SumAsync(selector, cancellationToken)
            : _dbSet.Where(predicate).SumAsync(selector, cancellationToken);

    public Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) =>
        _dbSet.AddRangeAsync(entities, cancellationToken);

    public ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        _dbSet.AddAsync(entity, cancellationToken);

    public void Update(TEntity entity) => _dbSet.Update(entity);

    public void Update(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);

    public void Delete(TEntity entity) => _dbSet.Remove(entity);

    public void Delete(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);
}