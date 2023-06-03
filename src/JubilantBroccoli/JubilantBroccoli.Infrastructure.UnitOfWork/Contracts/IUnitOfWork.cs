using JubilantBroccoli.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;

public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : DbContext
{
    TContext DbContext { get; }

    Task<int> SaveChangesAsync();
}

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync();

    SaveChangesResult LastSaveChangesResult { get; }
}